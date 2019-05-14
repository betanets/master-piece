using System.Collections.Generic;

namespace master_piece.lexeme
{
    /// <summary>
    /// Сущность лексемы
    /// </summary>
    class Lexeme
    {
        /// <summary>
        /// Тип лексемы
        /// </summary>
        public LexemeType lexemeType { get; }

        /// <summary>
        /// Текст лексемы
        /// </summary>
        public string lexemeText { get; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="lexemeType">Тип лексемы</param>
        /// <param name="lexemeText">Текст лексемы</param>
        public Lexeme(LexemeType lexemeType, string lexemeText)
        {
            this.lexemeType = lexemeType;
            this.lexemeText = lexemeText;
        }

        /// <summary>
        /// Метод сравнения.
        /// Определяет, равен ли указанный объект текущему объекту
        /// true, если объекты равны, false - в противном случае
        /// </summary>
        /// <param name="obj">Объект, с которым требуется сравнить текущий объект</param>
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

        /// <summary>
        /// Хэш-функция
        /// </summary>
        public override int GetHashCode()
        {
            var hashCode = -2056466745;
            hashCode = hashCode * -1521134295 + lexemeType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lexemeText);
            return hashCode;
        }
    }
}
