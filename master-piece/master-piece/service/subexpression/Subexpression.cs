using master_piece.lexeme;
using System;
using System.Collections.Generic;
using static master_piece.service.Operation;

namespace master_piece.service
{
    class Subexpression
    {
        public Lexeme lexemeFirst { get; }
        public OperationEnum operation { get; }
        public Lexeme lexemeSecond { get; }

        public Subexpression subexpressionFirst { get; }
        public Subexpression subexpressionSecond { get; }

        //Expression's serial number in list
        public int expressionLevel { get; }

        public bool mustBePrecalculated { get; set; } = false;

        /// <summary>
        /// Expression's value. Remains null until calculated
        /// </summary>
        public bool? value = null;

        /// <summary>
        /// Is expression major - not a part of another subexpression
        /// </summary>
        public bool major { get; set; } = false;

        //Leaf subexpression
        public Subexpression(Lexeme lexemeFirst, OperationEnum operation, Lexeme lexemeSecond, int expressionLevel)
        {
            this.lexemeFirst = lexemeFirst;
            this.operation = operation;
            this.lexemeSecond = lexemeSecond;
            this.expressionLevel = expressionLevel;
        }

        //Ordinary subexpression
        public Subexpression(Subexpression subexpressionFirst, OperationEnum operation, Subexpression subexpressionSecond, int expressionLevel)
        {
            this.subexpressionFirst = subexpressionFirst;
            this.operation = operation;
            this.subexpressionSecond = subexpressionSecond;
            this.expressionLevel = expressionLevel;
        }

        //Mixed subexpression
        public Subexpression(Subexpression subexpressionFirst, OperationEnum operation, Lexeme lexemeSecond, int expressionLevel)
        {
            this.subexpressionFirst = subexpressionFirst;
            this.operation = operation;
            this.lexemeSecond = lexemeSecond;
            this.expressionLevel = expressionLevel;
        }

        //Is leaf expression (contains only lemexes and operation)
        public bool isLeaf()
        {
            return lexemeFirst != null && lexemeSecond != null && subexpressionFirst == null && subexpressionSecond == null;
        }

        //Is mixed expression (contains 1 subexpression, 1 operation 1 lexeme) 
        public bool isMixed()
        {
            return lexemeFirst == null && lexemeSecond != null && subexpressionFirst != null && subexpressionSecond == null;
        }

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
