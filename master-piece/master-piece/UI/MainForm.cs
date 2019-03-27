using master_piece.domain;
using master_piece.lexeme;
using master_piece.service;
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

        public MainForm()
        {
            //TODO: move DB connection to separate file
            var databasePath = Path.Combine(Environment.CurrentDirectory, "mp.db"); //TODO: change path later

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.CreateTable<LinguisticVariable>();
            dbConnection.CreateTable<FuzzyVariable>();
            dbConnection.CreateTable<FuzzyVariableValue>();


            InitializeComponent();
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

        private void button_process_Click(object sender, EventArgs e)
        {
            richTextBox_log.Clear();

            List<IntVariable> intVariablesStorage = new List<IntVariable>();
            List<Subexpression> subexpressions = new List<Subexpression>();

            foreach (DataGridViewRow dgvr in dataGridView_intVariables.Rows)
            {
                if (dgvr.Index == dataGridView_intVariables.NewRowIndex)
                {
                    break;
                }

                //TODO: add additional checkers
                richTextBox_log.AppendText("\n\n----------Ввод переменных:----------------\n");
                IntVariable intVariable = new IntVariable(dgvr.Cells[0].Value.ToString(), Convert.ToInt32(dgvr.Cells[1].Value.ToString()));
                intVariablesStorage.Add(intVariable);
                richTextBox_log.AppendText("Переменная: " + intVariable.name + ", тип: " + intVariable.value + "\n");
            }

            int i = 1;
            foreach (DataGridViewRow dgvr in dataGridView_expressions.Rows)
            {
                if(dgvr.Index == dataGridView_expressions.NewRowIndex)
                {
                    break;
                }

                //Checking IF expression
                richTextBox_log.AppendText("\n\n----------Обработка выражения ЕСЛИ: " + dgvr.Cells[0].Value.ToString() + "----------------\n");
                ParserResult parserResult = ParserService.parseIfExpression(dgvr.Cells[0].Value.ToString());

                foreach (Lexeme lexeme in parserResult.lexemesList)
                {
                    richTextBox_log.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
                }

                SemanticResult semanticResult = SemanticService.makeSemanticAnalysis(parserResult, intVariablesStorage);
                richTextBox_log.AppendText("\n----------\nРезультаты семантического анализа\n-----------\n");
                if (!semanticResult.isCorrect)
                {
                    foreach (string str in semanticResult.output)
                    {
                        richTextBox_log.AppendText(str);
                    }

                    richTextBox_log.AppendText("\n----------\nНайдено некорректное выражение. Работа анализатора остановлена\n");
                    return;
                } else
                {
                    richTextBox_log.AppendText("Семантический анализ успешно выполнен\n");
                }

                //We should continue only if semantic analysis is correct
                richTextBox_log.AppendText("\n----------\nРезультаты представления в обратной польской записи\n-----------\n");

                List<Lexeme> reversePolishNotationLexemeList = ReversePolishNotationService.createNotation(parserResult.lexemesList);
                foreach (Lexeme lexeme in reversePolishNotationLexemeList)
                {
                    richTextBox_log.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
                }

                richTextBox_log.AppendText("\n----------\nСписок подвыражений\n-----------\n");

                List<Subexpression> currentSubexpressions = SubexpressionService.createSubexpressionsList(reversePolishNotationLexemeList, i);
                foreach (Subexpression subexpression in currentSubexpressions)
                {
                    richTextBox_log.AppendText(subexpression.ToString() + "\n");
                }
                subexpressions.AddRange(currentSubexpressions);


                //Checking THEN expression
                richTextBox_log.AppendText("\n\n----------Обработка выражения ТО: " + dgvr.Cells[1].Value.ToString() + "----------------\n");
                ParserResult thenParserResult = ParserService.parseThenExpression(dgvr.Cells[1].Value.ToString());

                foreach (Lexeme lexeme in thenParserResult.lexemesList)
                {
                    richTextBox_log.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
                }

                i++;
            }

            List<Subexpression> duplicates = new List<Subexpression>();
            richTextBox_log.AppendText("\n----------\nДубликаты подвыражений\n-----------\n");
            duplicates.AddRange(DuplicateExpressionService.findDuplicates(subexpressions));
            foreach (Subexpression d in duplicates)
            {
                richTextBox_log.AppendText(d.ToString() + "\n");
            }
        }
    }
}
