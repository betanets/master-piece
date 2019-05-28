using master_piece.service.import_export;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static master_piece.service.subexpression.Operation;

namespace master_piece.service.generation
{
    class GenerationService
    {
        private static List<OperationEnum> comparisonOperations = getComparisonOperations();
        private static List<OperationEnum> logicalOperations = getLogicalOperations();
        private static int comparisonOperationsCount = comparisonOperations.Count;
        private static int logicalOperationsCount = logicalOperations.Count;

        private static Random rand = new Random();
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static List<string> generateExpressions(List<string> variableNames, int expressionsCount)
        {
            List<string> expressions = new List<string>();

            for (int i = 0; i < expressionsCount; i++)
            {
                string expression = "";

                //IF expression generation
                int subexpressionsCount = 3 + rand.Next(10);
                for (int j = 0; j < subexpressionsCount; j++)
                {
                    int subexpressionType = rand.Next(3) % 3;
                    //Leaf
                    if (subexpressionType == 0)
                    {
                        expression += generateLeafSubexpression(variableNames);
                    }
                    //Mixed
                    else if (subexpressionType == 1)
                    {
                        int lexemePosition = rand.Next(2);
                        string lexeme = variableNames[rand.Next(variableNames.Count)];
                        string operation = getLexemeTextByOperation(logicalOperations[rand.Next(logicalOperationsCount)]);
                        if (lexemePosition == 0)
                        {
                            expression += "(" + lexeme + " " + operation + " "
                                + generateLeafSubexpression(variableNames) + ")";
                        }
                        else
                        {
                            expression += "(" + generateLeafSubexpression(variableNames)
                                + " " + operation + " " + lexeme + ")";
                        }
                    }
                    //Ordinary
                    else
                    {
                        string operation = getLexemeTextByOperation(logicalOperations[rand.Next(logicalOperationsCount)]);
                        expression += "(" + generateLeafSubexpression(variableNames)
                            + " " + operation + " " + generateLeafSubexpression(variableNames) + ")";
                    }
                    if (j != subexpressionsCount - 1)
                    {
                        expression += " " + getLexemeTextByOperation(logicalOperations[rand.Next(logicalOperationsCount)]) + " ";
                    }
                }

                //THEN expression generation
                expression += "\t\t" + generateThenOrElseExpression(variableNames);

                //ELSE expression generation
                int generateElse = rand.Next(2);
                if(generateElse == 1)
                {
                    expression += "\t\t" + generateThenOrElseExpression(variableNames);
                }

                expressions.Add(expression);
            }

            return expressions;
        }

        public static string generateLeafSubexpression(List<string> variableNames)
        {
            string firstVariable = variableNames[rand.Next(variableNames.Count)];
            string secondVariable = variableNames[rand.Next(variableNames.Count)];
            string operation = getLexemeTextByOperation(comparisonOperations[rand.Next(comparisonOperationsCount)]);

            return "(" + firstVariable + " " + operation + " " + secondVariable + ")";
        }

        public static string generateThenOrElseExpression(List<string> variableNames)
        {
            int reassignOrAssignNew = rand.Next(2);
            string variableToAssign;
            if (reassignOrAssignNew == 0)
            {
                //Reassignment
                variableToAssign = variableNames[rand.Next(variableNames.Count)];
            }
            else
            {
                //Create new variable
                variableToAssign = generateRandomString(2);
            }

            int valueOrVariable = rand.Next(2);
            string value;
            if (valueOrVariable == 0)
            {
                //Generate value
                value = rand.Next(1000).ToString();
            }
            else
            {
                //Assign to another random variable
                //TODO: assign to fuzzy value
                value = variableNames[rand.Next(variableNames.Count)];
            }
            return variableToAssign + " = " + value;
        }

        /// <summary>
        /// Метод создания строки из случайных символов алфавита
        /// </summary>
        /// <param name="length">Длина строки</param>
        public static string generateRandomString(int length)
        {
            return new string(Enumerable.Repeat(alphabet, length).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        public static ImportExportResult exportGeneratedStrings(List<string> expressions)
        {
            if (expressions.Count == 0)
            {
                return new ImportExportResult(ImportExportResultStatus.Error, "Нет выражений для сохранения");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream saveStream = saveFileDialog.OpenFile();
                using (StreamWriter writer = new StreamWriter(saveStream))
                {
                    foreach (string expression in expressions)
                    {
                        writer.WriteLine(expression);
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, "Выражения успешно сгенерированы");
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }
    }
}
