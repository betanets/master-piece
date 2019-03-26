using master_piece.lexeme;
using System.Collections.Generic;
using static master_piece.service.Operation;

namespace master_piece.service
{
    class SubexpressionService
    {
        public static List<Subexpression> createSubexpressionsList(List<Lexeme> lexemes, int expressionLevel)
        {
            List<Subexpression> subexpressions = new List<Subexpression>();
            Stack<Subexpression> subexpressionsStack = new Stack<Subexpression>();
            Stack<Lexeme> lexemesStack = new Stack<Lexeme>();

            foreach (Lexeme lex in lexemes)
            {
                if (LexemeTypes.IsValue(lex.lexemeType) || LexemeTypes.IsIdentifier(lex.lexemeType))
                {
                    lexemesStack.Push(lex);
                }
                else if (LexemeTypes.IsOperation(lex.lexemeType))
                {
                    if (lexemesStack.Count >= 2)
                    {
                        Lexeme lexemeSecond = lexemesStack.Pop();
                        Lexeme lexemeFirst = lexemesStack.Pop();
                        Subexpression subexpression = new Subexpression(lexemeFirst, getOperationByLexeme(lex.lexemeText), lexemeSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                    else if (subexpressionsStack.Count >= 2)
                    {
                        Subexpression subexpressionSecond = subexpressionsStack.Pop();
                        Subexpression subexpressionFirst = subexpressionsStack.Pop();
                        Subexpression subexpression = new Subexpression(subexpressionFirst, getOperationByLexeme(lex.lexemeText), subexpressionSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                    else if(lexemesStack.Count == 1 && subexpressionsStack.Count == 1)
                    {
                        Subexpression subexpressionFirst = subexpressionsStack.Pop();
                        Lexeme lexemeSecond = lexemesStack.Pop();
                        Subexpression subexpression = new Subexpression(subexpressionFirst, getOperationByLexeme(lex.lexemeText), lexemeSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                }
            }

            return subexpressions;
        }
    }
}
