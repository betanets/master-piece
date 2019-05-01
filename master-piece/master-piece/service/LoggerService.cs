using master_piece.lexeme;
using master_piece.subexpression;
using master_piece.variable;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.service
{
    class LoggerService
    {
        /// <summary>
        /// Integer variables logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="intVariablesStorage">Integer variables storage</param>
        public static void logIntVariables(RichTextBox loggerComponent, List<IntVariable> intVariablesStorage)
        {
            foreach (IntVariable intVariable in intVariablesStorage)
            {
                loggerComponent.AppendText("Переменная: " + intVariable.name + ", значение: " + intVariable.value + "\n");
            }
        }

        /// <summary>
        /// Expressions logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="expressionsStorage">Expressions storage</param>
        public static void logExpressions(RichTextBox loggerComponent, List<Expression> expressionsStorage)
        {
            foreach (Expression expression in expressionsStorage)
            {
                loggerComponent.AppendText("Выражение " + expression.expressionLevel + ": ЕСЛИ " + expression.ifExpressionText + ", ТО " + expression.thenExpressionText
                    + (expression.elseExpressionText.Equals(string.Empty) ? "\n" : ", ИНАЧЕ " + expression.elseExpressionText + "\n"));
            }
        }

        /// <summary>
        /// Parser service logging method for IF expressions
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="expression">Expression parsed by service</param>
        /// <param name="lexemesList">List of lexemes obtained using the service</param>
        public static void logIfParser(RichTextBox loggerComponent, string expression, List<Lexeme> lexemesList)
        {
            loggerComponent.AppendText("\n\n----------Обработка выражения ЕСЛИ: " + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Parser service logging method for THEN expressions
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="expression">Expression parsed by service</param>
        /// <param name="lexemesList">List of lexemes obtained using the service</param>
        public static void logThenParser(RichTextBox loggerComponent, string expression, List<Lexeme> lexemesList)
        {
            loggerComponent.AppendText("\n\n----------Обработка выражения ТО: " + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Semantic analysis result logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="semanticResult">Semantic analysis result</param>
        public static void logSemantic(RichTextBox loggerComponent, SemanticResult semanticResult)
        {
            loggerComponent.AppendText("\n----------\nРезультаты семантического анализа\n-----------\n");
            if (!semanticResult.isCorrect)
            {
                foreach (string str in semanticResult.output)
                {
                    loggerComponent.AppendText(str);
                }

                loggerComponent.AppendText("\n----------\nНайдено некорректное выражение. Работа анализатора остановлена\n");
            }
            else
            {
                loggerComponent.AppendText("Семантический анализ успешно выполнен\n");
            }
        }

        /// <summary>
        /// Reverse polish notation logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="reversePolishNotationResult">Reverse polish notation sorting result</param>
        public static void logReversePolishNotation(RichTextBox loggerComponent, List<Lexeme> reversePolishNotationResult)
        {
            loggerComponent.AppendText("\n----------\nРезультаты представления в обратной польской записи\n-----------\n");
            foreach (Lexeme lexeme in reversePolishNotationResult)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

        /// <summary>
        /// Subexpressions logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="subexpressions">List of subexpressions obtained by service</param>
        public static void logSubexpressions(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nСписок подвыражений\n-----------\n");

            foreach (Subexpression subexpression in subexpressions)
            {
                loggerComponent.AppendText(subexpression.ToString() + "\n");
            }
        }

        /// <summary>
        /// Variables list logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="intVariables">List of integer variables</param>
        public static void logAssignedVariables(RichTextBox loggerComponent, List<IntVariable> intVariables)
        {
            loggerComponent.AppendText("\n\n----------Текущие значения переменных:----------------\n");
            foreach (IntVariable iv in intVariables)
            {
                loggerComponent.AppendText("Переменная: " + iv.name + ", тип: " + iv.value + ", переопределена в выражении: " + iv.firstReassignmentLevel + "\n");
            }
        }

        /// <summary>
        /// Duplicates logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="subexpressions">List of all subexpressions</param>
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
        /// Subexpressions values logging method
        /// </summary>
        /// <param name="loggerComponent"></param>
        /// <param name="subexpressions"></param>
        public static void logSubexpressionsValues(RichTextBox loggerComponent, List<Subexpression> subexpressions)
        {
            loggerComponent.AppendText("\n----------\nЗначения подвыражений\n-----------\n");

            foreach (Subexpression exp in subexpressions)
            {
                loggerComponent.AppendText(exp.ToString() + ", значение: " + exp.value + "\n");
            }
        }
    }
}
