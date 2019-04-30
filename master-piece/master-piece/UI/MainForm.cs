using master_piece.domain;
using master_piece.lexeme;
using master_piece.service;
using master_piece.subexpression;
using master_piece.variable;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace master_piece
{
    public partial class MainForm : Form
    {
        private SQLiteConnection dbConnection;

        List<IntVariable> intVariablesStorage = new List<IntVariable>();
        List<Expression> expressionsStorage = new List<Expression>();

        List<Subexpression> subexpressions = new List<Subexpression>();

        public MainForm()
        {
            //TODO: move DB connection to separate file
            var databasePath = Path.Combine(Environment.CurrentDirectory, "mp.db"); //TODO: change path later
            //Preparing DB commection
            dbConnection = new SQLiteConnection(databasePath);

            //Creating tables IF NOT EXISTS
            dbConnection.CreateTable<LinguisticVariable>();
            dbConnection.CreateTable<FuzzyVariable>();
            dbConnection.CreateTable<FuzzyVariableValue>();

            InitializeComponent();
        }

        /// <summary>
        /// Method to clear temporary variables
        /// </summary>
        private void clearWorkplace()
        {
            richTextBox_log.Clear();
            intVariablesStorage.Clear();
            expressionsStorage.Clear();
            subexpressions.Clear();
        }

        private void button_process_Click(object sender, EventArgs e)
        {
            clearWorkplace();

            //Init int variables
            intVariablesStorage.AddRange(
                InitVariablesService.initIntVariables(dataGridView_intVariables.Rows, dataGridView_intVariables.NewRowIndex, richTextBox_log)
            );

            //Init expressions
            expressionsStorage.AddRange(
                InitExpressionsService.initExpressions(dataGridView_expressions.Rows, dataGridView_expressions.NewRowIndex, richTextBox_log)
            );

            foreach (Expression expression in expressionsStorage)
            {
                //Checking IF expression
                ParserResult parserResult = ParserService.parseIfExpression(expression.ifExpressionText);
                LoggerService.logParser(richTextBox_log, expression.ifExpressionText, parserResult.lexemesList);

                SemanticResult semanticResult = SemanticService.makeSemanticAnalysis(parserResult, intVariablesStorage);
                LoggerService.logSemantic(richTextBox_log, semanticResult);

                //We should continue only if semantic analysis is correct
                if (!semanticResult.isCorrect) return;
                

                richTextBox_log.AppendText("\n----------\nРезультаты представления в обратной польской записи\n-----------\n");

                List<Lexeme> reversePolishNotationLexemeList = ReversePolishNotationService.createNotation(parserResult.lexemesList);
                foreach (Lexeme lexeme in reversePolishNotationLexemeList)
                {
                    richTextBox_log.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
                }

                richTextBox_log.AppendText("\n----------\nСписок подвыражений\n-----------\n");

                List<Subexpression> currentSubexpressions = SubexpressionService.createSubexpressionsList(reversePolishNotationLexemeList, expression.expressionLevel);
                foreach (Subexpression subexpression in currentSubexpressions)
                {
                    richTextBox_log.AppendText(subexpression.ToString() + "\n");
                }
                subexpressions.AddRange(currentSubexpressions);


                //Checking THEN expression
                richTextBox_log.AppendText("\n\n----------Обработка выражения ТО: " + expression.thenExpressionText + "----------------\n");
                ParserResult thenParserResult = ParserService.parseThenExpression(expression.thenExpressionText);

                foreach (Lexeme lexeme in thenParserResult.lexemesList)
                {
                    richTextBox_log.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
                }

                SemanticService.assignVariables(thenParserResult, intVariablesStorage, expression.expressionLevel);

                richTextBox_log.AppendText("\n\n----------Текущие значения переменных:----------------\n");
                foreach(IntVariable iv in intVariablesStorage)
                {
                    richTextBox_log.AppendText("Переменная: " + iv.name + ", тип: " + iv.value + ", переопределена в выражении: " + iv.firstReassignmentLevel + "\n");
                }
            }

            richTextBox_log.AppendText("\n----------\nДубликаты подвыражений\n-----------\n");
            DuplicateExpressionService.markDuplicates(subexpressions, intVariablesStorage);
            foreach (Subexpression exp in subexpressions)
            {
                if (exp.mustBePrecalculated)
                {
                    richTextBox_log.AppendText(exp.ToString() + ", уровень: " + exp.expressionLevel + "\n");
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void нечёткиеПеременныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VariablesList fuzzyVariablesList = new VariablesList(dbConnection);
            fuzzyVariablesList.ShowDialog();
        }
    }
}
