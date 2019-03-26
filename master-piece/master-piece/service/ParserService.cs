using master_piece.lexeme;
using System.Text.RegularExpressions;

namespace master_piece.service
{
    /**
     * Expression parser service
     */
    class ParserService
    {
        static bool isBigLetter(char symbol)
        {
            return symbol >= 'A' && symbol <= 'Z';
        }

        static bool isLittleLetter(char symbol)
        {
            return symbol >= 'a' && symbol <= 'z';
        }

        static bool isNumber(char symbol)
        {
            return symbol >= '0' && symbol <= '9';
        }

        static bool isIgnoredSymbol(char symbol)
        {
            return symbol == ' ' || symbol == '\t' || symbol == '\n';
        }

        private static ParserResult parserResult;

        //TODO: need more research to determine correct parser
        //TODO: combine same parts with parseThenExpression
        public static ParserResult parseIfExpression(string expression)
        {
            parserResult = new ParserResult();
            string symbolSavior = string.Empty;

            //Remove all whitespaces from expression
            expression = Regex.Replace(expression, @"\s+", "");

            //Parse expression
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '(':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.OpenBracket, c.ToString()));
                        break;
                    case ')':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.CloseBracket, c.ToString()));
                        break;
                    case '&':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        symbolSavior += c;
                        if (symbolSavior.Length == 2)
                        {
                            if (symbolSavior.Equals("&&"))
                            {
                                parserResult.lexemesList.Add(new Lexeme(LexemeType.And, symbolSavior));
                            }
                            else
                            {
                                parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
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
                                parserResult.lexemesList.Add(new Lexeme(LexemeType.Or, symbolSavior));
                            }
                            else
                            {
                                parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
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
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Equal, symbolSavior));
                                    break;
                                case '!':
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.NotEqual, symbolSavior));
                                    break;
                                case '>':
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.MoreOrEqual, symbolSavior));
                                    break;
                                case '<':
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.LessOrEqual, symbolSavior));
                                    break;
                                default:
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                                    break;
                            }
                            symbolSavior = string.Empty;
                        }
                        break;
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
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Assign, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                case '>':
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.More, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                case '<':
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Less, symbolSavior));
                                    symbolSavior = c.ToString();
                                    break;
                                default:
                                    if (isBigLetter(symbolSavior[0]) || isLittleLetter(symbolSavior[0]) || isNumber(symbolSavior[0]))
                                    {
                                        symbolSavior += c;
                                    }
                                    else
                                    {
                                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
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
                                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
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
            return parserResult;
        }

        public static ParserResult parseThenExpression(string expression)
        {
            parserResult = new ParserResult();
            string symbolSavior = string.Empty;

            //Remove all whitespaces from expression
            expression = Regex.Replace(expression, @"\s+", "");

            //Parse expression
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '=':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Assign, c.ToString()));
                        break;
                    case ',':
                        symbolSavior = checkSymbolSavior(symbolSavior);
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Comma, c.ToString()));
                        break;
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
                                parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
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

            return parserResult;
        }

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
                    parserResult.lexemesList.Add(new Lexeme(LexemeType.Identifier, symbolSavior));
                }
                else if(isNumber(symbolSavior[0]))
                {
                    parserResult.lexemesList.Add(new Lexeme(LexemeType.IntValue, symbolSavior));
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
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                        return string.Empty;
                    }
                }

                if(isAllNumbers)
                {
                    parserResult.lexemesList.Add(new Lexeme(LexemeType.IntValue, symbolSavior));
                    return string.Empty;
                }
                else
                {
                    if(!isNumber(symbolSavior[0]))
                    {
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Identifier, symbolSavior));
                    }
                    else
                    {
                        parserResult.lexemesList.Add(new Lexeme(LexemeType.Error, symbolSavior));
                    }
                }
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
