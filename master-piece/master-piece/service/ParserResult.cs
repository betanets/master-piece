using master_piece.lexeme;
using master_piece.variable;
using System.Collections.Generic;

namespace master_piece.service
{
    class ParserResult
    {
        public List<Lexeme> lexemesList = new List<Lexeme>();
        //TODO: initialize variablesList and subexpressionsList
        public List<AbstractVariable> variablesList = new List<AbstractVariable>();
        public List<CommonSubexpression> subexpressionsList = new List<CommonSubexpression>();
    }
}
