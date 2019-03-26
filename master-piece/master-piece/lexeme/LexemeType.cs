namespace master_piece.lexeme
{
    public enum LexemeType
    {
        //Operations
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
        //Delimiter
        Comma,
        //Values
        IntValue,
        FuzzyValue,
        //Identifier
        Identifier,
        //Errorenous lexeme type
        Error
    }

    public static class LexemeTypes
    {
        //Notice that Assign and Brackets' operations are NOT in this list
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
