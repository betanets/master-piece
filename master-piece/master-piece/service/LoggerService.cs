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
        /// Parser service logging method
        /// </summary>
        /// <param name="loggerComponent">Log output component</param>
        /// <param name="expression">Expression parsed by service</param>
        /// <param name="lexemesList">List of lexemes obtained using the service</param>
        public static void logParser(RichTextBox loggerComponent, string expression, List<Lexeme> lexemesList)
        {
            loggerComponent.AppendText("\n\n----------Обработка выражения ЕСЛИ: " + expression + "----------------\n");
            foreach (Lexeme lexeme in lexemesList)
            {
                loggerComponent.AppendText("Лексема: " + lexeme.lexemeText + ", тип: " + lexeme.lexemeType + "\n");
            }
        }

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
    }
}
