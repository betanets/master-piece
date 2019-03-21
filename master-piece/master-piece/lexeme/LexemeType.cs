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
        //Values
        IntValue,
        FuzzyValue,
        //Identifier
        Identifier,
        //Errorenous lexeme type
        Error
    }
}
