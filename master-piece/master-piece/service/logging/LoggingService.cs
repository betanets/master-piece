using master_piece.lexeme;
using master_piece.service.fuzzy_variable;
using master_piece.service.init_variables;
using master_piece.subexpression;
using master_piece.variable;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.service
{
    /// <summary>
    /// Сервис логирования 
    /// </summary>
    //TODO: вынести loggerComponent в поле, внести его в конструктор сервиса, использовать поле во всех методах
    class LoggingService
    {
        /// <summary>
        /// Метод логирования переменных
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static void logVariables(RichTextBox loggerComponent, VariablesStorage variablesStorage)
        {
            foreach (IntViewVariable intVariable in variablesStorage.intVariables)
            {
                loggerComponent.AppendText("Переменная: " + intVariable.name + ", значение: " + intVariable.value + "\n");
            }
            foreach (FuzzyViewVariable fuzzyVariable in variablesStorage.fuzzyVariables)
            {
                loggerComponent.AppendText("Переменная: " + fuzzyVariable.name + ", значение: " + fuzzyVariable.value + "\n");
            }
        }

        /// <summary>
        /// Метод логирования нечетких переменных
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="fuzzyVariables">Список представлений нечетких переменных</param>
        public static void logFuzzyVariables(RichTextBox loggerComponent, List<FuzzyViewVariable> fuzzyVariables)
        {
            foreach (FuzzyViewVariable fuzzyVariable in fuzzyVariables)
            {
                loggerComponent.AppendText("Переменная: " + fuzzyVariable.name + ", значение: " + fuzzyVariable.value + "\n");
            }
        }

        /// <summary>
        /// Метод логирования выражений
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="expressionsStorage">Список выражений</param>
        public static void logExpressions(RichTextBox loggerComponent, List<Expression> expressionsStorage)
        {
            foreach (Expression expression in expressionsStorage)
            {
                loggerComponent.AppendText("Выражение " + expression.expressionLevel + ": ЕСЛИ " + expression.ifExpressionText + ", ТО " + expression.thenExpressionText
                    + (expression.elseExpressionText.Equals(string.Empty) ? "\n" : ", ИНАЧЕ " + expression.elseExpressionText + "\n"));
            }
        }

        /// <summary>
        /// Метод логирования парсера для выражений ЕСЛИ
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="expression">Выражение в текстовом виде</param>
        /// <param name="lexemesList">Список лексем, полученных из выражения</param>
        public static void logIfParser(RichTextBox loggerComponent, string expression, List<Lexeme> lexemesList)
        {
            loggerComponent.AppendText("\n\n----------Обработка выражения ЕСЛИ: " + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования парсера для выражений ТО или ИНАЧЕ
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="expression">Выражение в текстовом виде</param>
        /// <param name="lexemesList">Список лексем, полученных из выражения</param>
        /// <param name="isThenExpression">true, если обрабатывается выражение ТО, false - если обрабатывается выражение ИНАЧЕ</param>
        public static void logThenOrElseParser(RichTextBox loggerComponent, string expression, List<Lexeme> lexemesList, bool isThenExpression)
        {
            loggerComponent.AppendText("\n\n----------Обработка выражения " + (isThenExpression ? "ТО: " : "ИНАЧЕ: ") + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования результатов работы лексического анализатора
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="semanticResult">Результат лексического анализа</param>
        public static void logLexicalAnalysis(RichTextBox loggerComponent, LexicalAnalysisResult lexicalAnalysisResult)
        {
            loggerComponent.AppendText("\n----------\nРезультаты лексического анализа\n-----------\n");
            if (!lexicalAnalysisResult.isCorrect)
            {
                foreach (string str in lexicalAnalysisResult.output)
                {
                    loggerComponent.AppendText(str);
                }

                loggerComponent.AppendText("\n----------\nНайдено некорректное выражение. Работа анализатора остановлена\n");
            }
            else
            {
                loggerComponent.AppendText("Лексический анализ успешно выполнен\n");
            }
        }

        /// <summary>
        /// Метод логирования преобразования в ПОЛИЗ 
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="reversePolishNotationResult">Список лексем, полученных после преобразования в ПОЛИЗ</param>
        public static void logReversePolishNotation(RichTextBox loggerComponent, List<Lexeme> reversePolishNotationResult)
        {
            loggerComponent.AppendText("\n----------\nРезультаты представления в ПОЛИЗ\n-----------\n");
            foreach (Lexeme lexeme in reversePolishNotationResult)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования подвыражений
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="subexpressions">Список подвыражений</param>
        public static void logSubexpressions(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nСписок подвыражений\n-----------\n");

            foreach (Subexpression subexpression in subexpressions)
            {
                loggerComponent.AppendText(subexpression.ToString() + ", тип: " + (subexpression.major ? "Главное" : "Подвыражение") + "\n");
            }
        }

        /// <summary>
        /// Метод логирования переменных с присвоенными им значениями
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="variables">Хранилище переменных</param>
        /// <param name="showReassignmentInfo">true, если нужно указать информацию об уровне переопределения, false в противном случае</param>
        public static void logAssignedVariables(RichTextBox loggerComponent, VariablesStorage variables, bool showReassignmentInfo)
        {
            loggerComponent.AppendText("\n\n----------Текущие значения переменных:----------------\n");
            foreach (IntViewVariable iv in variables.intVariables)
            {
                loggerComponent.AppendText("Переменная: " + iv.name + ", значение: " + iv.value 
                    + (showReassignmentInfo ? ", переопределена в выражении: " + iv.firstReassignmentLevel : "") + "\n");
            }
            foreach (FuzzyViewVariable iv in variables.fuzzyVariables)
            {
                loggerComponent.AppendText("Переменная: " + iv.name + ", значение: " + iv.value 
                    + (showReassignmentInfo ? ", переопределена в выражении: " + iv.firstReassignmentLevel : "") + "\n");
            }
        }

        /// <summary>
        /// Метод логирования дубликатов подвыражений
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="subexpressions">Список подвыражений</param>
        public static void logDuplicates(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nДубликаты подвыражений\n-----------\n");

            foreach (Subexpression exp in subexpressions)
            {
                if (exp.mustBePrecalculated)
                {
                    loggerComponent.AppendText(exp.ToString() + ", уровень: " + exp.expressionLevel + "\n");
                }
            }
        }

        /// <summary>
        /// Метод логирования значений дубликатов подвыражений
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="subexpressions">Список подвыражений</param>
        public static void logDuplicatesValues(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nЗначения дубликатов подвыражений\n-----------\n");

            foreach (Subexpression exp in subexpressions)
            {
                if (exp.mustBePrecalculated)
                {
                    loggerComponent.AppendText(exp.ToString() + ", значение: " + exp.value + "\n");
                }
            }
        }

        /// <summary>
        /// Метод логирования значений подвыражений
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="subexpressions">Список подвыражений</param>
        public static void logSubexpressionsValues(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nЗначения подвыражений\n-----------\n");

            foreach (Subexpression exp in subexpressions)
            {
                loggerComponent.AppendText(exp.ToString() + ", значение: " + exp.value + "\n");
            }
        }

        /// <summary>
        /// Метод логирования результата подбора нечеткой переменной по значению
        /// </summary>
        /// <param name="loggerComponent">Компонент, в который выводится лог</param>
        /// <param name="selectionResult">Результат подбора нечеткой переменной по значению</param>
        public static void logFuzzySelectionError(RichTextBox loggerComponent, FuzzyVariableSelectionResult selectionResult)
        {
            loggerComponent.AppendText("\n----------\nПодбор нечётких переменных по значению\n-----------\n");
            loggerComponent.AppendText(selectionResult.messageString + "\n");
        }
    }
}
