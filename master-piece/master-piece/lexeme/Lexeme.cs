namespace master_piece.lexeme
{
    class Lexeme
    {
        public LexemeType lexemeType { get; }
        public string lexemeText { get; }

        public Lexeme(LexemeType lexemeType, string lexemeText)
        {
            this.lexemeType = lexemeType;
            this.lexemeText = lexemeText;
        }
    }
}
