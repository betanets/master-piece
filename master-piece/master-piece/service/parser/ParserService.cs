using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace master_piece.service.parser
{
    /// <summary>
    /// Сервис по созданию списка лексем из выражений в текстовом виде
    /// </summary>
    class ParserService
    {
        /// <summary>
        /// true, если символ - большая буква, false - в противном случе
        /// </summary>
        /// <param name="symbol">Проверяемый символ</param>
        static bool isBigLetter(char symbol)
        {
            return symbol >= 'A' && symbol <= 'Z';
        }

        /// <summary>
        /// true, если символ - маленькая буква, false - в противном случе
        /// </summary>
        /// <param name="symbol">Проверяемый символ</param>
        static bool isLittleLetter(char symbol)
        {
            return symbol >= 'a' && symbol <= 'z';
        }

        /// <summary>
        /// true, если символ - цифра, false - в противном случе
        /// </summary>
        /// <param name="symbol">Проверяемый символ</param>
        static bool isNumber(char symbol)
        {
            return symbol >= '0' && symbol <= '9';
        }

        /// <summary>
        /// true, если символ является игнорируемым, false - в противном случе
        /// </summary>
        /// <param name="symbol">Проверяемый символ</param>
        static bool isIgnoredSymbol(char symbol)
        {
            return symbol == ' ' || symbol == '\t' || symbol == '\n';
        }

        private static List<Lexeme> lexemesList;

        /// <summary>
        /// Метод по созданию списка лексем из выражения ЕСЛИ
        /// </summary>
        /// <param name="expression">Выражение ЕСЛИ в текстовом виде</param>
        //TODO: need more research to determine correct parser
        //TODO: combine same parts with parseThenExpression
        public static List<Lexeme> parseIfExpression(string expression)
        {
            lexemesList = new List<Lexeme>();
            string symbolSavior = string.Empty;

            //Remove all whitespaces from expression
            expression = Regex.Replace(expression, @"\s+", "");

            //Parse expression
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                bool noNeedToCheckSavior = false;
                switch (c)
                {
                    case '"':
                        //Parse fuzzy value. Symbol savior must be empty
                        symbolSavior = c.ToString();
                        noNeedToCheckSavior = true;
                        i++;
                        while (i != expression.Length)
                        {
                            char currentSym = expression[i];
                            symbolSavior += currentSym;
                            if (currentSym == '"')
                            {
                                break;
                            }
                            i++;
                        }
                        lexemesList.Add(new Lexeme(LexemeType.FuzzyValue, symbolSavior));
                        //Cleanup symbol savior
                        symbolSavior = string.Empty;
                        break;
                    case '(':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        lexemesList.Add(new Lexeme(LexemeType.OpenBracket, c.ToString()));
                        break;
                    case ')':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        lexemesList.Add(new Lexeme(LexemeType.CloseBracket, c.ToString()));
                        break;
                    case '&':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        symbolSavior += c;
                        if (symbolSavior.Length == 2)
                        {
                            if (symbolSavior.Equals("&&"))
                            {
                                lexemesList.Add(new Lexeme(LexemeType.And, symbolSavior));
                            }
                            else
                            {
                                lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                            }
                            symbolSavior = string.Empty;
                        }
                        break;
                    case '|':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        symbolSavior += c;
                        if (symbolSavior.Length == 2)
                        {
                            if (symbolSavior.Equals("||"))
                            {
                                lexemesList.Add(new Lexeme(LexemeType.Or, symbolSavior));
                            }
                            else
                            {
                                lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                            }
                            symbolSavior = string.Empty;
                        }
                        break;
                    case '!':
                    case '>':
                    case '<':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        symbolSavior += c;
                        break;
                    case '=':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        symbolSavior += c;
                        if (symbolSavior.Length == 2)
                        {
                            switch (symbolSavior[0])
                            {
                                case '=':
                                    lexemesList.Add(new Lexeme(LexemeType.Equal, symbolSavior));
                                    break;
                                case '!':
                                    lexemesList.Add(new Lexeme(LexemeType.NotEqual, symbolSavior));
                                    break;
                                case '>':
                                    lexemesList.Add(new Lexeme(LexemeType.MoreOrEqual, symbolSavior));
                                    break;
                                case '<':
                                    lexemesList.Add(new Lexeme(LexemeType.LessOrEqual, symbolSavior));
                                    break;
                                default:
                                    lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                                    break;
                            }
                            symbolSavior = string.Empty;
                        }
                        break;
                }
                if(noNeedToCheckSavior)
                {
                    continue;
                }

                if (isBigLetter(c) || isLittleLetter(c) || isNumber(c))
                {
                    if (symbolSavior.Length > 0)
                    {
                        if (symbolSavior.Length == 1)
                        {
                            switch (symbolSavior[0])
                            {
                                case '=':
                                    lexemesList.Add(new Lexeme(LexemeType.Assign, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                case '>':
                                    lexemesList.Add(new Lexeme(LexemeType.More, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                case '<':
                                    lexemesList.Add(new Lexeme(LexemeType.Less, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                default:
                                    if (isBigLetter(symbolSavior[0]) || isLittleLetter(symbolSavior[0]) || isNumber(symbolSavior[0]))
                                    {
                                        symbolSavior += c;
                                    }
                                    else
                                    {
                                        lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                                        symbolSavior = string.Empty;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            symbolSavior += c;
                            foreach (char sym in symbolSavior)
                            {
                                if (!isBigLetter(sym) && !isLittleLetter(sym) && !isNumber(sym))
                                {
                                    lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                                    symbolSavior = string.Empty;
                                }
                            }
                        }
                    }
                    else
                    {
                        symbolSavior = c.ToString();
                    }
                }
            }

            if(symbolSavior.Length > 0)
            {
                checkSymbolSavior(symbolSavior);
            }
            return lexemesList;
        }

        /// <summary>
        /// Метод по созданию списка лексем из выражения ТО или ИНАЧЕ
        /// </summary>
        /// <param name="expression">Выражение ТО или ИНАЧЕ в текстовом виде</param>
        public static List<Lexeme> parseThenOrElseExpression(string expression)
        {
            lexemesList = new List<Lexeme>();
            string symbolSavior = string.Empty;

            //Remove all whitespaces from expression
            expression = Regex.Replace(expression, @"\s+", "");

            //Parse expression
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                bool noNeedToCheckSavior = false;
                switch (c)
                {
                    case '"':
                        //Parse fuzzy value. Symbol savior must be empty
                        symbolSavior = c.ToString();
                        noNeedToCheckSavior = true;
                        i++;
                        while (i != expression.Length)
                        {
                            char currentSym = expression[i];
                            symbolSavior += currentSym;
                            i++;
                            if (currentSym == '"')
                            {
                                break;
                            }
                        }
                        lexemesList.Add(new Lexeme(LexemeType.FuzzyValue, symbolSavior));
                        //Cleanup symbol savior
                        symbolSavior = string.Empty;
                        break;
                    case '=':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        lexemesList.Add(new Lexeme(LexemeType.Assign, c.ToString()));
                        break;
                    case ',':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        lexemesList.Add(new Lexeme(LexemeType.Comma, c.ToString()));
                        break;
                }

                if (noNeedToCheckSavior)
                {
                    continue;
                }

                if (isBigLetter(c) || isLittleLetter(c) || isNumber(c))
                {
                    if (symbolSavior.Length > 0)
                    {
                        symbolSavior += c;
                        foreach (char sym in symbolSavior)
                        {
                            if (!isBigLetter(sym) && !isLittleLetter(sym) && !isNumber(sym))
                            {
                                lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                                symbolSavior = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        symbolSavior = c.ToString();
                    }
                }
            }

            if (symbolSavior.Length > 0)
            {
                checkSymbolSavior(symbolSavior);
            }

            return lexemesList;
        }

        /// <summary>
        /// Метод по проверке хранилища символов.
        /// Используется только внутри данного класса как вспомогательный метод при обработке идентификаторов, целочисленных и нечетких значений
        /// </summary>
        /// <param name="symbolSavior">Хранилище символов</param>
        private static string checkSymbolSavior(string symbolSavior)
        {
            if(symbolSavior.Length == 0)
            {
                return string.Empty;
            }
            if(symbolSavior.Length == 1)
            {
                if(isBigLetter(symbolSavior[0]) || isLittleLetter(symbolSavior[0]))
                {
                    lexemesList.Add(new Lexeme(LexemeType.Identifier, symbolSavior));
                }
                else if(isNumber(symbolSavior[0]))
                {
                    lexemesList.Add(new Lexeme(LexemeType.IntValue, symbolSavior));
                }
                else
                {
                    return symbolSavior;
                }
                return string.Empty;
            }
            if(symbolSavior.Length > 1)
            {
                bool isAllNumbers = true;
                foreach (char sym in symbolSavior)
                {
                    if(isBigLetter(sym) || isLittleLetter(sym))
                    {
                        isAllNumbers = false;
                    }
                    else if(isNumber(sym))
                    {
                        //OK, do nothing
                    }
                    else
                    {
                        lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                        return string.Empty;
                    }
                }

                if(isAllNumbers)
                {
                    lexemesList.Add(new Lexeme(LexemeType.IntValue, symbolSavior));
                    return string.Empty;
                }
                else
                {
                    if(!isNumber(symbolSavior[0]))
                    {
                        lexemesList.Add(new Lexeme(LexemeType.Identifier, symbolSavior));
                    }
                    else
                    {
                        lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                    }
                }
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
