using master_piece.lexeme;
using System;
using System.Collections.Generic;
using static master_piece.service.Operation;

namespace master_piece.service
{
    /// <summary>
    /// Сущность подвыражения
    /// </summary>
    class Subexpression
    {
        /// <summary>
        /// Первая лексема
        /// </summary>
        public Lexeme lexemeFirst { get; }
        
        /// <summary>
        /// Операция в подвыражении
        /// </summary>
        public OperationEnum operation { get; }

        /// <summary>
        /// Вторая лексема
        /// </summary>
        public Lexeme lexemeSecond { get; }

        /// <summary>
        /// Первое вложенное подвыражение
        /// </summary>
        public Subexpression subexpressionFirst { get; }

        /// <summary>
        /// Второе вложенное подвыражение
        /// </summary>
        public Subexpression subexpressionSecond { get; }

        /// <summary>
        /// Порядковый номер выражения, к которому принадлежит данное подвыражение
        /// </summary>
        public int expressionLevel { get; }

        /// <summary>
        /// Флаг, указывающий, что выражение следует вычислять до непосредственной работы с нечеткими правилами.
        /// По умолчанию: false
        /// </summary>
        public bool mustBePrecalculated { get; set; } = false;

        /// <summary>
        /// Значение подвыражения.
        /// По умолчанию: null
        /// </summary>
        public bool? value = null;

        /// <summary>
        /// true, если подвыражение - главное (не является частью другого подвыражения), false - в противном случае
        /// </summary>
        public bool major { get; set; } = false;

        /// <summary>
        /// Конструктор подвыражения типа "лист"
        /// </summary>
        /// <param name="lexemeFirst">Первая лексема</param>
        /// <param name="operation">Операция</param>
        /// <param name="lexemeSecond">Вторая лексема</param>
        /// <param name="expressionLevel">Порядковый номер выражения</param>
        public Subexpression(Lexeme lexemeFirst, OperationEnum operation, Lexeme lexemeSecond, int expressionLevel)
        {
            this.lexemeFirst = lexemeFirst;
            this.operation = operation;
            this.lexemeSecond = lexemeSecond;
            this.expressionLevel = expressionLevel;
        }

        /// <summary>
        /// Конструктор обычного подвыражения
        /// </summary>
        /// <param name="subexpressionFirst">Первое подвыражение</param>
        /// <param name="operation">Операция</param>
        /// <param name="subexpressionSecond">Второе подвыражение</param>
        /// <param name="expressionLevel">Порядковый номер выражения</param>
        public Subexpression(Subexpression subexpressionFirst, OperationEnum operation, Subexpression subexpressionSecond, int expressionLevel)
        {
            this.subexpressionFirst = subexpressionFirst;
            this.operation = operation;
            this.subexpressionSecond = subexpressionSecond;
            this.expressionLevel = expressionLevel;
        }

        /// <summary>
        /// Конструктор подвыражения смешанного типа
        /// </summary>
        /// <param name="subexpressionFirst">Первое подвыражение</param>
        /// <param name="operation">Операция</param>
        /// <param name="lexemeSecond">Вторая лексема</param>
        /// <param name="expressionLevel">Порядковый номер выражения</param>
        public Subexpression(Subexpression subexpressionFirst, OperationEnum operation, Lexeme lexemeSecond, int expressionLevel)
        {
            this.subexpressionFirst = subexpressionFirst;
            this.operation = operation;
            this.lexemeSecond = lexemeSecond;
            this.expressionLevel = expressionLevel;
        }

        /// <summary>
        /// true, если выражение имеет тип "лист", false - в противном случае
        /// </summary>
        public bool isLeaf()
        {
            return lexemeFirst != null && lexemeSecond != null && subexpressionFirst == null && subexpressionSecond == null;
        }

        /// <summary>
        /// true, если выражение - смешанного типа, false - в противном случае
        /// </summary>
        public bool isMixed()
        {
            return lexemeFirst == null && lexemeSecond != null && subexpressionFirst != null && subexpressionSecond == null;
        }

        /// <summary>
        /// Метод преобразования подвыражения в строку
        /// </summary>
        public override string ToString()
        {
            if (isLeaf())
            {
                return "(" + lexemeFirst.lexemeText + " " + operation.ToString() + " " + lexemeSecond.lexemeText + ")";
            }
            else if (isMixed())
            {
                return "(" + subexpressionFirst.ToString() + " " + operation.ToString() + " " + lexemeSecond.lexemeText + ")";
            }
            else {
                return "(" + subexpressionFirst.ToString() + " " + operation.ToString() + " " + subexpressionSecond.ToString() + ")";
            }
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
            var compared = obj as Subexpression;

            if (isLeaf() && compared.isLeaf())
            {
                //Comparison for leaf expressions
                if (lexemeFirst.Equals(compared.lexemeFirst) && lexemeSecond.Equals(compared.lexemeSecond) && operation.Equals(compared.operation))
                {
                    return true;
                }
                return false;
            }
            else if (isMixed() && compared.isMixed())
            {
                //Comparison for mixed expressions
                if (subexpressionFirst.Equals(compared.subexpressionFirst) && lexemeSecond.Equals(compared.lexemeSecond) && operation.Equals(compared.operation))
                {
                    return true;
                }
                return false;
            }
            else if(!isLeaf() && !isMixed() && !compared.isLeaf() && !compared.isMixed())
            {
                //Comparison for ordinary expressions
                if(subexpressionFirst.Equals(compared.subexpressionFirst) && subexpressionSecond.Equals(compared.subexpressionSecond) && operation.Equals(compared.operation))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Хэш-функция
        /// </summary>
        public override int GetHashCode()
        {
            var hashCode = -322050275;
            hashCode = hashCode * -1521134295 + EqualityComparer<Lexeme>.Default.GetHashCode(lexemeFirst);
            hashCode = hashCode * -1521134295 + operation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Lexeme>.Default.GetHashCode(lexemeSecond);
            hashCode = hashCode * -1521134295 + EqualityComparer<Subexpression>.Default.GetHashCode(subexpressionFirst);
            hashCode = hashCode * -1521134295 + EqualityComparer<Subexpression>.Default.GetHashCode(subexpressionSecond);
            hashCode = hashCode * -1521134295 + expressionLevel.GetHashCode();
            return hashCode;
        }
    }
}
