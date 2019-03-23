using master_piece.domain;
using master_piece.lexeme;
using master_piece.service;
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

        private void button_add_Click(object sender, EventArgs e)
        {
            ParserResult parserResult = ParserService.parse(textBox_expression.Text);
            richTextBox_result.Clear();
            foreach (Lexeme lexeme in parserResult.lexemesList)
            {
                richTextBox_result.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }

            richTextBox_result.AppendText("\n----------\nРезультаты представления в обратной польской записи\n-----------\n");

            List <Lexeme> reversePolishNotationLexemeList = ReversePolishNotationService.createNotation(parserResult.lexemesList);
            foreach (Lexeme lexeme in reversePolishNotationLexemeList)
            {
                richTextBox_result.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }

            richTextBox_result.AppendText("\n----------\nСписок подвыражений\n-----------\n");

            List<Subexpression> subexpressions = SubexpressionService.createSubexpressionsList(reversePolishNotationLexemeList);
            foreach (Subexpression subexpression in subexpressions)
            {
                richTextBox_result.AppendText(subexpression.ToString() + "\n");
            }
        }
    }
}
