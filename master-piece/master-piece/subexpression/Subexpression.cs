using master_piece.lexeme;
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

        //Is leaf expression (contains only lemexes and operation)
        public bool isLeaf()
        {
            return lexemeFirst != null && lexemeSecond != null && subexpressionFirst == null && subexpressionSecond == null;
        }

        public override string ToString()
        {
            if (isLeaf())
            {
                return "(" + lexemeFirst.lexemeText + " " + operation.ToString() + " " + lexemeSecond.lexemeText + ")";
            }
            else
            {
                return "(" + subexpressionFirst.ToString() + " " + operation.ToString() + " " + subexpressionSecond.ToString() + ")";
            }
        }
    }
}
