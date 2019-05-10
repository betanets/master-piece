using master_piece.lexeme;
using System;
using System.Collections.Generic;

namespace master_piece.service
{
    [Obsolete("Use List<Lexeme> instead")]
    class ParserResult
    {
        public List<Lexeme> lexemesList = new List<Lexeme>();
        //TODO: initialize variablesList and subexpressionsList
        //public List<AbstractVariable> variablesList = new List<AbstractVariable>();
        //public List<Subexpression> subexpressionsList = new List<Subexpression>();
    }
}
