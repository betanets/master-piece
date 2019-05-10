using System.Collections.Generic;
using System.Linq;

namespace master_piece.service
{
    public static class Operation
    {
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

        public static OperationEnum getOperationByLexeme(string lexeme)
        {
            return separators.FirstOrDefault(dictionary => dictionary.Value == lexeme).Key;
        }
    }
}
