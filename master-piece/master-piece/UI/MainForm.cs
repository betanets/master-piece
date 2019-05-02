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
        List<IntVariable> intVariablesStorage_holder = new List<IntVariable>();

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
            intVariablesStorage_holder.Clear();
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

            //Copy int variables into holder
            intVariablesStorage_holder.AddRange(intVariablesStorage);

            //Init expressions
            expressionsStorage.AddRange(
                InitExpressionsService.initExpressions(dataGridView_expressions.Rows, dataGridView_expressions.NewRowIndex, richTextBox_log)
            );

            foreach (Expression expression in expressionsStorage)
            {
                //Checking IF expression
                ParserResult parserResult = ParserService.parseIfExpression(expression.ifExpressionText);
                LoggerService.logIfParser(richTextBox_log, expression.ifExpressionText, parserResult.lexemesList);

                //Checking semantic
                SemanticResult semanticResult = SemanticService.makeSemanticAnalysis(parserResult, intVariablesStorage);
                LoggerService.logSemantic(richTextBox_log, semanticResult);

                //Next steps are available only if semantic analysis is correct
                if (!semanticResult.isCorrect) return;

                //Sorting by reverse polish notation
                List<Lexeme> reversePolishNotationLexemeList = ReversePolishNotationService.createNotation(parserResult.lexemesList);
                LoggerService.logReversePolishNotation(richTextBox_log, reversePolishNotationLexemeList);

                //Creating subexpressions
                List<Subexpression> currentSubexpressions = SubexpressionService.createSubexpressionsList(reversePolishNotationLexemeList, expression.expressionLevel);
                LoggerService.logSubexpressions(richTextBox_log, currentSubexpressions);
                subexpressions.AddRange(currentSubexpressions);

                //Checking THEN expression
                ParserResult thenParserResult = ParserService.parseThenOrElseExpression(expression.thenExpressionText);
                LoggerService.logThenOrElseParser(richTextBox_log, expression.thenExpressionText, parserResult.lexemesList, true);


                //Variable assignment
                //We should assign variables now to correctly mark duplicates
                //We have no idea now whether THEN or ELSE expression will be executed
                SemanticService.assignVariables(thenParserResult, intVariablesStorage, expression.expressionLevel);
                LoggerService.logAssignedVariables(richTextBox_log, intVariablesStorage, true);

                //Checking ELSE expression
                ParserResult elseParserResult = ParserService.parseThenOrElseExpression(expression.elseExpressionText);
                LoggerService.logThenOrElseParser(richTextBox_log, expression.thenExpressionText, parserResult.lexemesList, false);

                SemanticService.assignVariables(elseParserResult, intVariablesStorage, expression.expressionLevel);
                LoggerService.logAssignedVariables(richTextBox_log, intVariablesStorage, true);
            }

            //Marking duplicates
            DuplicateExpressionService.markDuplicates(subexpressions, intVariablesStorage);
            LoggerService.logDuplicates(richTextBox_log, subexpressions);

            //Precalculating duplicates
            SubexpressionService.calculateDuplicates(subexpressions, intVariablesStorage);
            LoggerService.logDuplicatesValues(richTextBox_log, subexpressions);

            //Restore int variables storage to init state
            intVariablesStorage.Clear();
            intVariablesStorage.AddRange(intVariablesStorage_holder);

            richTextBox_log.AppendText("------========РЕЗУЛЬТАТЫ:========---------\n");
            //Calculating major subexpressions one by one
            //TODO: check level and compare it with local counter to avoid wrong order
            foreach (Subexpression subexpression in subexpressions)
            {
                if (subexpression.major)
                {
                    //Calculate subexpression
                    subexpression.value = SubexpressionService.calculateSubexpressionValue(subexpression, intVariablesStorage);
                    LoggerService.logSubexpressions(richTextBox_log, subexpressions);

                    //Prepare int variables storage to next iteration
                    ParserResult parserResult;
                    if (subexpression.value.Value)
                    {
                        parserResult = ParserService.parseThenOrElseExpression(expressionsStorage[subexpression.expressionLevel - 1].thenExpressionText);
                    }
                    else
                    {
                        parserResult = ParserService.parseThenOrElseExpression(expressionsStorage[subexpression.expressionLevel - 1].elseExpressionText);
                    }
                    SemanticService.assignVariables(parserResult, intVariablesStorage, subexpression.expressionLevel);
                }
            }
            LoggerService.logAssignedVariables(richTextBox_log, intVariablesStorage, false);
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
