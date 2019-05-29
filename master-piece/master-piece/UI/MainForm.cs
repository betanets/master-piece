using master_piece.domain;
using master_piece.service.duplicate;
using master_piece.service.fuzzy_variable;
using master_piece.service.import_export;
using master_piece.service.init_expressions;
using master_piece.service.init_variables;
using master_piece.service.lexical_analysis;
using master_piece.service.logging;
using master_piece.service.parser;
using master_piece.service.reverse_polish_notation;
using master_piece.service.subexpression;
using master_piece.UI;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace master_piece
{
    public partial class MainForm : Form
    {
        private SQLiteConnection dbConnection;
        private FuzzyVariableService fuzzyVariableService;
        private SubexpressionService subexpressionService;
        private LoggingService loggingService;

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
            //Init subexpression service
            subexpressionService = new SubexpressionService(dbConnection);

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
            var watch = Stopwatch.StartNew();

            loggingService = new LoggingService(richTextBox_log);
            loggingService.noLogMode = checkBox_measureExecutionTime.Checked;
            clearWorkplace();

            //Init int and fuzzy variables
            VariablesStorage variablesStorage = 
                InitVariablesService.initVariables(dataGridView_intVariables.Rows, dataGridView_intVariables.NewRowIndex, loggingService);

            //Select fuzzy variables by their names
            FuzzyVariableSelectionResult selectionResult = fuzzyVariableService.makeSelection(variablesStorage);
            if(!selectionResult.isSuccess)
            {
                loggingService.logFuzzySelectionError(selectionResult);
                return;
            }

            //Copy int and fuzzy variables into holder
            variablesStorage_holder.intVariables.AddRange(variablesStorage.intVariables);
            variablesStorage_holder.fuzzyVariables.AddRange(variablesStorage.fuzzyVariables);

            //Init expressions
            expressionsStorage.AddRange(
                InitExpressionsService.initExpressions(dataGridView_expressions.Rows, dataGridView_expressions.NewRowIndex, loggingService)
            );

            foreach (Expression expression in expressionsStorage)
            {
                //Checking IF expression
                List<Lexeme> ifParserResult = ParserService.parseIfExpression(expression.ifExpressionText);
                loggingService.logIfParser(expression.ifExpressionText, ifParserResult);

                //Checking parser result on fuzzy values existence
                FuzzyVariableSelectionResult ifSelectionResult = fuzzyVariableService.makeSelection(ifParserResult);
                if (!ifSelectionResult.isSuccess)
                {
                    loggingService.logFuzzySelectionError(ifSelectionResult);
                    return;
                }

                //Checking semantic
                LexicalAnalysisResult semanticResult = LexicalAnalysisService.makeSemanticAnalysis(ifParserResult, variablesStorage);
                loggingService.logLexicalAnalysis(semanticResult);

                //Next steps are available only if semantic analysis is correct
                if (!semanticResult.isCorrect) return;

                //Sorting by reverse polish notation
                List<Lexeme> reversePolishNotationLexemeList = ReversePolishNotationService.createNotation(ifParserResult);
                loggingService.logReversePolishNotation(reversePolishNotationLexemeList);

                //Creating subexpressions
                List<Subexpression> currentSubexpressions = subexpressionService.createSubexpressionsList(reversePolishNotationLexemeList, expression.expressionLevel);
                loggingService.logSubexpressions(currentSubexpressions);
                subexpressions.AddRange(currentSubexpressions);

                //Checking THEN expression
                List<Lexeme> thenParserResult = ParserService.parseThenOrElseExpression(expression.thenExpressionText);
                loggingService.logThenOrElseParser(expression.thenExpressionText, thenParserResult, true);


                //Variable assignment
                //We should assign variables now to correctly mark duplicates
                //We have no idea now whether THEN or ELSE expression will be executed
                LexicalAnalysisService.assignVariables(thenParserResult, variablesStorage, expression.expressionLevel);
                loggingService.logAssignedVariables(variablesStorage, true);

                //Select fuzzy variables by their names
                FuzzyVariableSelectionResult thenSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                if (!thenSelectionResult.isSuccess)
                {
                    loggingService.logFuzzySelectionError(thenSelectionResult);
                    return;
                }

                //Checking ELSE expression
                List<Lexeme> elseParserResult = ParserService.parseThenOrElseExpression(expression.elseExpressionText);
                loggingService.logThenOrElseParser(expression.elseExpressionText, elseParserResult, false);

                LexicalAnalysisService.assignVariables(elseParserResult, variablesStorage, expression.expressionLevel);
                loggingService.logAssignedVariables(variablesStorage, true);

                //Select fuzzy variables by their names
                FuzzyVariableSelectionResult elseSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                if (!elseSelectionResult.isSuccess)
                {
                    loggingService.logFuzzySelectionError(elseSelectionResult);
                    return;
                }
            }

            //Marking duplicates
            DuplicateExpressionService.markDuplicates(subexpressions, variablesStorage);
            loggingService.logDuplicates(subexpressions);

            if (!checkBox_disableBoosters.Checked)
            {
                //Precalculating duplicates
                subexpressionService.calculateDuplicates(subexpressions, variablesStorage, !checkBox_disableBoosters.Checked);
                loggingService.logDuplicatesValues(subexpressions);
            }

            //Restore int variables storage to init state
            variablesStorage.Clear();
            variablesStorage.intVariables.AddRange(variablesStorage_holder.intVariables);
            variablesStorage.fuzzyVariables.AddRange(variablesStorage_holder.fuzzyVariables);

            //Calculating major subexpressions one by one
            //TODO: check level and compare it with local counter to avoid wrong order
            foreach (Subexpression subexpression in subexpressions)
            {
                if (subexpression.major)
                {
                    //Calculate subexpression
                    subexpression.value = subexpressionService.calculateSubexpressionValue(subexpression, variablesStorage, !checkBox_disableBoosters.Checked);

                    //Prepare int variables storage to next iteration
                    List<Lexeme> parserResult;
                    if (subexpression.value.Value)
                    {
                        parserResult = ParserService.parseThenOrElseExpression(expressionsStorage[subexpression.expressionLevel - 1].thenExpressionText);
                    }
                    else
                    {
                        parserResult = ParserService.parseThenOrElseExpression(expressionsStorage[subexpression.expressionLevel - 1].elseExpressionText);
                    }
                    LexicalAnalysisService.assignVariables(parserResult, variablesStorage, subexpression.expressionLevel);

                    //Select fuzzy variables by their names
                    FuzzyVariableSelectionResult sfvSelectionResult = fuzzyVariableService.makeSelection(variablesStorage);
                    if (!sfvSelectionResult.isSuccess)
                    {
                        loggingService.logFuzzySelectionError(sfvSelectionResult);
                        return;
                    }
                }
            }

            loggingService.logString("------========РЕЗУЛЬТАТЫ:========---------\n");
            loggingService.logSubexpressions(subexpressions);

            loggingService.noLogMode = false;
            loggingService.logAssignedVariables(variablesStorage, false);

            watch.Stop();
            if (checkBox_measureExecutionTime.Checked)
            {
                loggingService.logExecutionTime(watch.ElapsedMilliseconds);
            }
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

        /// <summary>
        /// Expression generation call method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ВыраженияToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GenerateForm generateForm = new GenerateForm();
            generateForm.ShowDialog();
        }
    }
}
