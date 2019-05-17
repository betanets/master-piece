using System;

namespace master_piece.service.parser
{
    /// <summary>
    /// Перечисление типов лексем
    /// </summary>
    public enum LexemeType
    {
        //Операции
        OpenBracket,
        CloseBracket,
        Assign,
        And,
        Or,
        Equal,
        NotEqual,
        More,
        MoreOrEqual,
        Less,
        LessOrEqual,
        //Разделитель
        Comma,
        //Значения
        IntValue,
        FuzzyValue,
        //Идентификатор
        Identifier,
        //Ошибочный тип лексемы
        Error
    }

    /// <summary>
    /// Вспомогательный класс по определению супертипа лексемы
    /// </summary>
    public static class LexemeTypes
    {
        /// <summary>
        /// true, если переданный тип является операцией, false - в противном случае
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        public static bool IsOperation(LexemeType lexemeType)
        {
            switch (lexemeType)
            {
                case LexemeType.And:
                case LexemeType.Or:
                case LexemeType.Equal:
                case LexemeType.NotEqual:
                case LexemeType.More:
                case LexemeType.MoreOrEqual:
                case LexemeType.Less:
                case LexemeType.LessOrEqual:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// true, если переданный тип является целочисленным значением, false - в противном случае
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        public static bool IsIntValue(LexemeType lexemeType)
        {
            return lexemeType == LexemeType.IntValue;
        }

        /// <summary>
        /// true, если переданный тип является нечетким значением, false - в противном случае
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        public static bool IsFuzzyValue(LexemeType lexemeType)
        {
            return lexemeType == LexemeType.FuzzyValue;
        }

        /// <summary>
        /// true, если переданный тип является нечетким значением, false - в противном случае
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        [Obsolete("Следует использовать IsIntValue или IsFuzzyValue")]
        public static bool IsValue(LexemeType lexemeType)
        {
            switch (lexemeType)
            {
                case LexemeType.IntValue:
                case LexemeType.FuzzyValue:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// true, если переданный тип является идентификатором, false - в противном случае
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        public static bool IsIdentifier(LexemeType lexemeType)
        {
            switch (lexemeType)
            {
                case LexemeType.Identifier:
                    return true;
                default:
                    return false;
            }
        }
    }
}
