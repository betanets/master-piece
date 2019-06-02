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
        /// <summary>
        /// Список операций сравнения
        /// </summary>
        private static List<OperationEnum> comparisonOperations = getComparisonOperations();

        /// <summary>
        /// Список логических операций
        /// </summary>
        private static List<OperationEnum> logicalOperations = getLogicalOperations();

        /// <summary>
        /// Количество операций сравнения
        /// </summary>
        private static int comparisonOperationsCount = comparisonOperations.Count;

        /// <summary>
        /// Количество логических операций
        /// </summary>
        private static int logicalOperationsCount = logicalOperations.Count;

        /// <summary>
        /// Генератор псевдослучайных элементов
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Алфавит для создания переменных со случайным именем
        /// </summary>
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Основной метод генерации выражений.
        /// Возвращает выражения в виде списка строк
        /// </summary>
        /// <param name="variableNames">Список имен исходных переменных</param>
        /// <param name="expressionsCount">Требуемое количество выражений</param>
        /// <param name="allowReuse">Флаг, указывающий, что разрешено создавать переменные со случайным именем</param>
        /// <param name="ifBlockCountFrom">Кол-во подвыражений в блоке ЕСЛИ (ОТ)</param>
        /// <param name="ifBlockCountTo">Кол-во подвыражения в блоке ЕСЛИ (ДО)</param>
        /// <param name="thenBlockCountFrom">Кол-во присваиваний в блоке ТО (ОТ)</param>
        /// <param name="thenBlockCountTo">Кол-во присваиваний в блоке ТО (ДО)</param>
        /// <param name="elseBlockCountFrom">Кол-во присваиваний в блоке ИНАЧЕ (ОТ)</param>
        /// <param name="elseBlockCountTo">Кол-во присваиваний в блоке ИНАЧЕ (ДО)</param>
        /// <param name="fuzzyVariableNames">Список имен нечетких переменных</param>
        /// <param name="allowOnlyFuzzy">Флаг, указывающий, что следует генерировать только выражения с нечеткими переменными</param>
        public static List<string> generateExpressions(List<string> variableNames, int expressionsCount, bool allowReuse, 
            int ifBlockCountFrom, int ifBlockCountTo, int thenBlockCountFrom, int thenBlockCountTo,
            int elseBlockCountFrom, int elseBlockCountTo, List<string> fuzzyVariableNames, bool allowOnlyFuzzy)
        {
            List<string> expressions = new List<string>();

            for (int i = 0; i < expressionsCount; i++)
            {
                string expression = "";

                //IF expression generation
                int subexpressionsCount = ifBlockCountFrom + rand.Next(ifBlockCountTo - ifBlockCountFrom + 1);
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
                int countOfThenExpressions = thenBlockCountFrom + rand.Next(thenBlockCountTo - thenBlockCountFrom + 1);
                expression += "\t\t";
                for (int j = 0; j < countOfThenExpressions; j++)
                {
                    expression += generateThenOrElseExpression(variableNames, allowReuse, allowOnlyFuzzy, fuzzyVariableNames);
                    if (j != countOfThenExpressions - 1)
                    {
                        expression += ", ";
                    }
                }

                //ELSE expression generation
                int generateElse = rand.Next(2);
                if (generateElse == 1)
                {
                    int countOfElseExpressions = elseBlockCountFrom + rand.Next(elseBlockCountTo - elseBlockCountFrom + 1);
                    expression += "\t\t";
                    for (int j = 0; j < countOfElseExpressions; j++)
                    {
                        expression += generateThenOrElseExpression(variableNames, allowReuse, allowOnlyFuzzy, fuzzyVariableNames);
                        if (j != countOfElseExpressions - 1)
                        {
                            expression += ", ";
                        }
                    }
                }

                expressions.Add(expression);
            }

            return expressions;
        }

        /// <summary>
        /// Метод генерации подвыражения типа "лист".
        /// Возвращает строку-подвыражение
        /// </summary>
        /// <param name="variableNames">Список имен переменных</param>
        public static string generateLeafSubexpression(List<string> variableNames)
        {
            string firstVariable = variableNames[rand.Next(variableNames.Count)];
            string secondVariable = variableNames[rand.Next(variableNames.Count)];
            string operation = getLexemeTextByOperation(comparisonOperations[rand.Next(comparisonOperationsCount)]);

            return "(" + firstVariable + " " + operation + " " + secondVariable + ")";
        }

        /// <summary>
        /// Метод генерации выражения ТО или ИНАЧЕ.
        /// Возвразает выражение в виде строки
        /// </summary>
        /// <param name="variableNames">Список имен переменных</param>
        /// <param name="allowReuse">Флаг, указывающий, что разрешено создавать переменные со случайным именем</param>
        /// <param name="allowOnlyFuzzy">Флаг, указывающий, что следует генерировать только выражения с нечеткими переменными</param>
        /// <param name="fuzzyVariableNames">Список имен нечетких переменных</param>
        public static string generateThenOrElseExpression(List<string> variableNames, bool allowReuse, bool allowOnlyFuzzy, List<string> fuzzyVariableNames)
        {
            bool rerun = true;
            
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
                if (allowReuse)
                {
                    variableNames.Add(variableToAssign);
                }
            }

            string value = "";
            while (rerun)
            {
                int valueOrVariable = (fuzzyVariableNames.Count > 0) ? ((allowOnlyFuzzy ? 1 + rand.Next(2) : rand.Next(3))) : rand.Next(2);
                if (valueOrVariable == 0)
                {
                    //Generate int value
                    value = rand.Next(1000).ToString();
                }
                else if (valueOrVariable == 1)
                {
                    //Assign to another random variable
                    value = variableNames[rand.Next(variableNames.Count)];
                }
                else
                {
                    //Assign to random fuzzy value
                    value = "\"" + fuzzyVariableNames[rand.Next(fuzzyVariableNames.Count)] + "\"";
                }

                if (variableToAssign != value)
                {
                    rerun = false;
                }
            }
            return variableToAssign + " = " + value;
        }

        /// <summary>
        /// Метод создания строки из случайных символов алфавита.
        /// Возвращает строку указанной длины
        /// </summary>
        /// <param name="length">Длина строки</param>
        public static string generateRandomString(int length)
        {
            return new string(Enumerable.Repeat(alphabet, length).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Метод экспорта сгенерированных выражений в файл.
        /// Возвращает сущность <see cref="ImportExportResult"/>
        /// </summary>
        /// <param name="expressions">Список выражений для экспорта</param>
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
