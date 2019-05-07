using master_piece.domain;
using master_piece.lexeme;
using master_piece.service;
using master_piece.service.fuzzy_variable;
using master_piece.service.import_export;
using master_piece.service.init_variables;
using master_piece.subexpression;
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
        private FuzzyVariableService fuzzyVariableService;

        VariablesStorage variablesStorage = new VariablesStorage();
        VariablesStorage variablesStorage_holder = new VariablesStorage();

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

            //Init fuzzy variable service
            fuzzyVariableService = new FuzzyVariableService(dbConnection);

            InitializeComponent();
        }

        /// <summary>
        /// Method to clear temporary variables
        /// </summary>
        private void clearWorkplace()
        {
            richTextBox_log.Clear();
            variablesStorage.Clear();
            variablesStorage_holder.Clear();
            expressionsStorage.Clear();
            subexpressions.Clear();
        }

        private void button_process_Click(object sender, EventArgs e)
        {
            clearWorkplace();

            //Init int and fuzzy variables
            VariablesStorage variablesStorage = 
                InitVariablesService.initVariables(dataGridView_intVariables.Rows, dataGridView_intVariables.NewRowIndex, richTextBox_log);

            //Select fuzzy variables by their names
            FuzzyVariableSelectionResult selectionResult = fuzzyVariableService.makeSelection(variablesStorage);
            if(!selectionResult.isSuccess)
            {
                LoggerService.logFuzzySelectionError(richTextBox_log, selectionResult);
                return;
            }

            //Copy int and fuzzy variables into holder
            variablesStorage_holder.intVariables.AddRange(variablesStorage.intVariables);
            variablesStorage_holder.fuzzyVariables.AddRange(variablesStorage.fuzzyVariables);

            //Init expressions
            expressionsStorage.AddRange(
                InitExpressionsService.initExpressions(dataGridView_expressions.Rows, dataGridView_expressions.NewRowIndex, richTextBox_log)
            );

            foreach (Expression expression in expressionsStorage)
            {
                //Checking IF expression
                ParserResult parserResult = ParserService.parseIfExpression(expression.ifExpressionText);
                LoggerService.logIfParser(richTextBox_log, expression.ifExpressionText, parserResult.lexemesList);

                //Checking parser result on fuzzy values existence
                FuzzyVariableSelectionResult ifSelectionResult = fuzzyVariableService.makeSelection(parserResult.lexemesList);
                if (!ifSelectionResult.isSuccess)
                {
                    LoggerService.logFuzzySelectionError(richTextBox_log, ifSelectionResult);
                    return;
                }

                //Checking semantic
                SemanticResult semanticResult = SemanticService.makeSemanticAnalysis(parserResult, variablesStorage);
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
                LoggerService.logThenOrElseParser(richTextBox_log, expression.thenExpressionText, thenParserResult.lexemesList, true);


                //Variable assignment
                //We should assign variables now to correctly mark duplicates
                //We have no idea now whether THEN or ELSE expression will be executed
                SemanticService.assignVariables(thenParserResult, variablesStorage, expression.expressionLevel);
                LoggerService.logAssignedVariables(richTextBox_log, variablesStorage, true);

                //Select fuzzy variables by their names
                FuzzyVariableSelectionResult thenSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                if (!thenSelectionResult.isSuccess)
                {
                    LoggerService.logFuzzySelectionError(richTextBox_log, thenSelectionResult);
                    return;
                }

                //Checking ELSE expression
                ParserResult elseParserResult = ParserService.parseThenOrElseExpression(expression.elseExpressionText);
                LoggerService.logThenOrElseParser(richTextBox_log, expression.elseExpressionText, elseParserResult.lexemesList, false);

                SemanticService.assignVariables(elseParserResult, variablesStorage, expression.expressionLevel);
                LoggerService.logAssignedVariables(richTextBox_log, variablesStorage, true);

                //Select fuzzy variables by their names
                FuzzyVariableSelectionResult elseSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                if (!elseSelectionResult.isSuccess)
                {
                    LoggerService.logFuzzySelectionError(richTextBox_log, elseSelectionResult);
                    return;
                }
            }

            //Marking duplicates
            DuplicateExpressionService.markDuplicates(subexpressions, variablesStorage);
            LoggerService.logDuplicates(richTextBox_log, subexpressions);

            //Precalculating duplicates
            SubexpressionService.calculateDuplicates(subexpressions, variablesStorage);
            LoggerService.logDuplicatesValues(richTextBox_log, subexpressions);

            //Restore int variables storage to init state
            variablesStorage.Clear();
            variablesStorage.intVariables.AddRange(variablesStorage_holder.intVariables);
            variablesStorage.fuzzyVariables.AddRange(variablesStorage_holder.fuzzyVariables);

            richTextBox_log.AppendText("------========РЕЗУЛЬТАТЫ:========---------\n");
            //Calculating major subexpressions one by one
            //TODO: check level and compare it with local counter to avoid wrong order
            foreach (Subexpression subexpression in subexpressions)
            {
                if (subexpression.major)
                {
                    //Calculate subexpression
                    subexpression.value = SubexpressionService.calculateSubexpressionValue(subexpression, variablesStorage);
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
                    SemanticService.assignVariables(parserResult, variablesStorage, subexpression.expressionLevel);

                    //Select fuzzy variables by their names
                    FuzzyVariableSelectionResult sfvSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                    if (!sfvSelectionResult.isSuccess)
                    {
                        LoggerService.logFuzzySelectionError(richTextBox_log, sfvSelectionResult);
                        return;
                    }
                }
            }
            LoggerService.logAssignedVariables(richTextBox_log, variablesStorage, false);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void лингвистическиеПеременныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VariablesList fuzzyVariablesList = new VariablesList(dbConnection);
            fuzzyVariablesList.ShowDialog();
        }

        /// <summary>
        /// Variables import call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void переменныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportExportResult result = ImportExportService.importVariables(dataGridView_intVariables);
            if(result.status.Equals(ImportExportResultStatus.Error))
            {
                richTextBox_log.AppendText(result.messageString + '\n');
                MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Expressions import call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выраженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportExportResult result = ImportExportService.importExpressions(dataGridView_expressions);
            if (result.status.Equals(ImportExportResultStatus.Error))
            {
                richTextBox_log.AppendText(result.messageString + '\n');
                MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Variables export call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void переменныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportExportResult result = ImportExportService.exportVariables(dataGridView_intVariables);
            switch(result.status)
            {
                case ImportExportResultStatus.Success:
                    MessageBox.Show(result.messageString, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ImportExportResultStatus.Error:
                    MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        /// Expressions export call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выраженияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportExportResult result = ImportExportService.exportExpressions(dataGridView_expressions);
            switch (result.status)
            {
                case ImportExportResultStatus.Success:
                    MessageBox.Show(result.messageString, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ImportExportResultStatus.Error:
                    MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        /// Results export call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void результатыОбработкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportExportResult result = ImportExportService.exportResults(richTextBox_log);
            switch (result.status)
            {
                case ImportExportResultStatus.Success:
                    MessageBox.Show(result.messageString, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ImportExportResultStatus.Error:
                    MessageBox.Show(result.messageString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
