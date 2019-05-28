using System.Collections.Generic;
using System.Linq;

namespace master_piece.service.subexpression
{
    /// <summary>
    /// Вспомогательный класс по работе с операциями
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// Перечисление операций
        /// </summary>
        public enum OperationEnum
        {
            Assign,
            And,
            Or,
            Equal,
            NotEqual,
            More,
            MoreOrEqual,
            Less,
            LessOrEqual
        }

        /// <summary>
        /// Словарь сопоставления перечисления операций с их текстовым представлением
        /// </summary>
        private static Dictionary<OperationEnum, string> dictionary = new Dictionary<OperationEnum, string>
        {
            { OperationEnum.Assign, "=" },
            { OperationEnum.And, "&&" },
            { OperationEnum.Or, "||" },
            { OperationEnum.Equal, "==" },
            { OperationEnum.NotEqual, "!=" },
            { OperationEnum.More, ">" },
            { OperationEnum.MoreOrEqual, ">=" },
            { OperationEnum.Less, "<" },
            { OperationEnum.LessOrEqual, "<=" }
        };

        /// <summary>
        /// Метод получения операции по лексеме
        /// </summary>
        /// <param name="lexeme">Текст лексемы</param>
        public static OperationEnum getOperationByLexeme(string lexeme)
        {
            return dictionary.FirstOrDefault(dictionary => dictionary.Value == lexeme).Key;
        }

        /// <summary>
        /// Метод получения текста лексемы по операции
        /// </summary>
        /// <param name="operation">Операция</param>
        public static string getLexemeTextByOperation(OperationEnum operation)
        {
            return dictionary.FirstOrDefault(dictionary => dictionary.Key == operation).Value;
        }

        /// <summary>
        /// Метод получения списка операций сравнения
        /// </summary>
        public static List<OperationEnum> getComparisonOperations()
        {
            return new List<OperationEnum>() {
                OperationEnum.Less,
                OperationEnum.LessOrEqual,
                OperationEnum.More,
                OperationEnum.MoreOrEqual,
                OperationEnum.NotEqual,
                OperationEnum.Equal,
                OperationEnum.LessOrEqual
            };
        }

        /// <summary>
        /// Метод получения списка логических операций
        /// </summary>
        public static List<OperationEnum> getLogicalOperations()
        {
            return new List<OperationEnum>() {
                OperationEnum.And,
                OperationEnum.Or
            };
        }
    }
}
