using master_piece.variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return symbol >= 'A' && symbol <= 'Z';
        }

        static bool isNumber(char symbol)
        {
            return symbol >= '0' && symbol <= '9';
        }

        static bool isIgnoredSymbol(char symbol)
        {
            return symbol == ' ' || symbol == '\t' || symbol == '\n';
        }

        //TODO: need more research to determine correct parser
        void parse(string expression)
        {
            ParserResult parserResult = new ParserResult();
            string lexeme = string.Empty;
            AbstractVariable abstractVariable = null; 
            foreach(char c in expression)
            {
                if(lexeme.Length == 0 && isNumber(c))
                {
                    abstractVariable = new IntVariable();
                }
                lexeme += c;

                if (isIgnoredSymbol(c))
                {
                    if(abstractVariable != null)
                    {
                        parserResult.variablesList.Add(abstractVariable);
                        abstractVariable = null;
                        lexeme = string.Empty;
                    }
                }

                //If current symbol is letter - we're working with identifier
                if(isBigLetter(c) || isLittleLetter(c))
                {

                }
            }
        }
    }
}
