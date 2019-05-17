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
        public enum OperationEnum {
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
        private static Dictionary<OperationEnum, string> separators = new Dictionary<OperationEnum, string>
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
            return separators.FirstOrDefault(dictionary => dictionary.Value == lexeme).Key;
        }
    }
}
