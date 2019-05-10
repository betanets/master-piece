using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType() || this == null || obj == null)
            {
                return false;
            }
            var lexeme = obj as Lexeme;
            if (lexemeType == lexeme.lexemeType && lexemeText == lexeme.lexemeText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -2056466745;
            hashCode = hashCode * -1521134295 + lexemeType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lexemeText);
            return hashCode;
        }
    }
}
