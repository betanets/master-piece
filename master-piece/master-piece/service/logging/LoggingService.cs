using master_piece.service.fuzzy_variable;
using master_piece.service.init_expressions;
using master_piece.service.init_variables;
using master_piece.service.lexical_analysis;
using master_piece.service.parser;
using master_piece.service.subexpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace master_piece.service.logging
{
    /// <summary>
    /// Сервис логирования 
    /// </summary>
    class LoggingService
    {
        /// <summary>
        /// Флаг, отвечающий за включение/отключение логирования
        /// </summary>
        public bool noLogMode { get; set; } = false;

        /// <summary>
        /// Компонент вывода лога
        /// </summary>
        private RichTextBox loggerComponent;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="loggerComponent"></param>
        public LoggingService(RichTextBox loggerComponent)
        {
            this.loggerComponent = loggerComponent;
        }

        /// <summary>
        /// Метод логирования произвольной строки. Переводы строки в методе не добавляются
        /// </summary>
        /// <param name="str">Строка, которую требуется вывести в лог</param>
        public void logString(string str)
        {
            if (noLogMode) return;

            loggerComponent.AppendText(str);
        }

        /// <summary>
        /// Метод логирования строки с ошибкой. Переводы строки в методе не добавляются
        /// </summary>
        /// <param name="str">Строка, которую требуется вывести в лог</param>
        public void logError(string str)
        {
            loggerComponent.AppendText(str);
        }

        /// <summary>
        /// Мето логирования времени выполнения алгоритма
        /// </summary>
        /// <param name="executionTimeMilliseconds">Время выполнения алгоритма в миллисекундах</param>
        public void logExecutionTime(long executionTimeMilliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(executionTimeMilliseconds);
            string answer = string.Format("{0:D2}ч:{1:D2}м:{2:D2}сек:{3:D3}мс",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
            loggerComponent.AppendText("\n\nВремя выполнения: " + answer + "\n");
        }

        /// <summary>
        /// Метод логирования переменных
        /// </summary>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public void logVariables(VariablesStorage variablesStorage)
        {
            if (noLogMode) return;

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
        /// <param name="fuzzyVariables">Список представлений нечетких переменных</param>
        public void logFuzzyVariables(List<FuzzyViewVariable> fuzzyVariables)
        {
            if (noLogMode) return;

            foreach (FuzzyViewVariable fuzzyVariable in fuzzyVariables)
            {
                loggerComponent.AppendText("Переменная: " + fuzzyVariable.name + ", значение: " + fuzzyVariable.value + "\n");
            }
        }

        /// <summary>
        /// Метод логирования выражений
        /// </summary>
        /// <param name="expressionsStorage">Список выражений</param>
        public void logExpressions(List<Expression> expressionsStorage)
        {
            if (noLogMode) return;

            foreach (Expression expression in expressionsStorage)
            {
                loggerComponent.AppendText("Выражение " + expression.expressionLevel + ": ЕСЛИ " + expression.ifExpressionText + ", ТО " + expression.thenExpressionText
                    + (expression.elseExpressionText.Equals(string.Empty) ? "\n" : ", ИНАЧЕ " + expression.elseExpressionText + "\n"));
            }
        }

        /// <summary>
        /// Метод логирования парсера для выражений ЕСЛИ
        /// </summary>
        /// <param name="expression">Выражение в текстовом виде</param>
        /// <param name="lexemesList">Список лексем, полученных из выражения</param>
        public void logIfParser(string expression, List<Lexeme> lexemesList)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n\n----------Обработка выражения ЕСЛИ: " + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования парсера для выражений ТО или ИНАЧЕ
        /// </summary>
        /// <param name="expression">Выражение в текстовом виде</param>
        /// <param name="lexemesList">Список лексем, полученных из выражения</param>
        /// <param name="isThenExpression">true, если обрабатывается выражение ТО, false - если обрабатывается выражение ИНАЧЕ</param>
        public void logThenOrElseParser(string expression, List<Lexeme> lexemesList, bool isThenExpression)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n\n----------Обработка выражения " + (isThenExpression ? "ТО: " : "ИНАЧЕ: ") + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования результатов работы лексического анализатора
        /// </summary>
        /// <param name="lexicalAnalysisResult">Результат лексического анализа</param>
        public void logLexicalAnalysis(LexicalAnalysisResult lexicalAnalysisResult)
        {
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
                if (noLogMode) return;
                loggerComponent.AppendText("\n----------\nРезультаты лексического анализа\n-----------\n");
                loggerComponent.AppendText("Лексический анализ успешно выполнен\n");
            }
        }

        /// <summary>
        /// Метод логирования преобразования в ПОЛИЗ 
        /// </summary>
        /// <param name="reversePolishNotationResult">Список лексем, полученных после преобразования в ПОЛИЗ</param>
        public void logReversePolishNotation(List<Lexeme> reversePolishNotationResult)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n----------\nРезультаты представления в ПОЛИЗ\n-----------\n");
            foreach (Lexeme lexeme in reversePolishNotationResult)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Метод логирования подвыражений
        /// </summary>
        /// <param name="subexpressions">Список подвыражений</param>
        public void logSubexpressions(List<Subexpression> subexpressions)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n----------\nСписок подвыражений\n-----------\n");

            foreach (Subexpression subexpression in subexpressions)
            {
                loggerComponent.AppendText(subexpression.ToString() + ", тип: " + (subexpression.major ? "Главное" : "Подвыражение") + "\n");
            }
        }

        /// <summary>
        /// Метод логирования переменных с присвоенными им значениями
        /// </summary>
        /// <param name="variables">Хранилище переменных</param>
        /// <param name="showReassignmentInfo">true, если нужно указать информацию об уровне переопределения, false в противном случае</param>
        public void logAssignedVariables(VariablesStorage variables, bool showReassignmentInfo)
        {
            if (noLogMode) return;

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
        /// <param name="subexpressions">Список подвыражений</param>
        public void logDuplicates(List<Subexpression> subexpressions)
        {
            if (noLogMode) return;

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
        /// <param name="subexpressions">Список подвыражений</param>
        public void logDuplicatesValues(List<Subexpression> subexpressions)
        {
            if (noLogMode) return;

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
        /// <param name="subexpressions">Список подвыражений</param>
        public void logSubexpressionsValues(List<Subexpression> subexpressions)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n----------\nЗначения подвыражений\n-----------\n");

            foreach (Subexpression exp in subexpressions)
            {
                loggerComponent.AppendText(exp.ToString() + ", значение: " + exp.value + "\n");
            }
        }

        /// <summary>
        /// Метод логирования результата подбора нечеткой переменной по значению
        /// </summary>
        /// <param name="selectionResult">Результат подбора нечеткой переменной по значению</param>
        public void logFuzzySelectionError(FuzzyVariableSelectionResult selectionResult)
        {
            if (noLogMode) return;

            loggerComponent.AppendText("\n----------\nПодбор нечётких переменных по значению\n-----------\n");
            loggerComponent.AppendText(selectionResult.messageString + "\n");
        }
    }
}
