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

        void parse(string expression)
        {
            string lexeme = string.Empty;

            foreach(char c in expression)
            {
                if(lexeme.Length == 0 && isNumber(c))
                {

                }
                lexeme += c;

                //If current symbol is letter or - we're working with identificator
                if(isBigLetter(c) || isLittleLetter(c) || (lexeme.Length > 0 && isNumber(c)))
                {

                }
            }
        }
    }
}
