using master_piece.service.parser;
using System.Collections.Generic;

namespace master_piece.service.reverse_polish_notation
{
    /// <summary>
    /// Сервис по работе с ПОЛИЗ
    /// </summary>
    class ReversePolishNotationService
    {
        /// <summary>
        /// Метод получения приоритета типа лексемы.
        /// Возвращает приоритет типа лексемы - целое число от 0 до 3
        /// </summary>
        /// <param name="lexemeType"></param>
        private static byte GetPriority(LexemeType lexemeType)
        {
            switch (lexemeType)
            {
                case LexemeType.OpenBracket:
                case LexemeType.CloseBracket:
                    return 0;
                case LexemeType.Equal:
                case LexemeType.NotEqual:
                case LexemeType.More:
                case LexemeType.MoreOrEqual:
                case LexemeType.Less:
                case LexemeType.LessOrEqual:
                    return 1;
                case LexemeType.And:
                case LexemeType.Or:
                    return 2;
                default:
                    return 3;
            }
        }

        /// <summary>
        /// Метод преобразования списка лексем из инфиксной записи в ПОЛИЗ.
        /// Возвращает список лексем, представленных в виде ПОЛИЗ.
        /// </summary>
        /// <param name="lexemes">Список лексем в инфиксной записи</param>
        public static List<Lexeme> createNotation(List<Lexeme> lexemes)
        {
            List<Lexeme> output = new List<Lexeme>();
            Stack<Lexeme> stack = new Stack<Lexeme>();

            foreach (Lexeme lex in lexemes)
            {
                if (GetPriority(lex.lexemeType) < 3)
                {
                    if (stack.Count > 0 && !lex.lexemeType.Equals(LexemeType.OpenBracket))
                    {
                        if (lex.lexemeType.Equals(LexemeType.CloseBracket)) {
                            Lexeme stackPop = stack.Pop();
                            while (stackPop.lexemeType != LexemeType.OpenBracket)
                            {
                                output.Add(stackPop);
                                stackPop = stack.Pop();
                            }
                        }
                        else
                        {
                            if (GetPriority(lex.lexemeType) > GetPriority(stack.Peek().lexemeType))
                            {
                                stack.Push(lex);
                            }
                            else
                            {
                                while (stack.Count > 0 && GetPriority(lex.lexemeType) <= GetPriority(stack.Peek().lexemeType))
                                {
                                    output.Add(stack.Pop());
                                } 
                                stack.Push(lex);
                            }
                        }
                    }
                    else
                    {
                        stack.Push(lex);
                    }
                }
                else
                {
                    output.Add(lex);
                }
            }

            if (stack.Count > 0)
            {
                foreach (Lexeme lex in stack)
                {
                    output.Add(lex);
                }
            }
            return output;
        }

    }
}
