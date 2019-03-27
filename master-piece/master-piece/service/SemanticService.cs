using master_piece.lexeme;
using master_piece.variable;
using System.Collections.Generic;

namespace master_piece.service
{
    class SemanticService
    {
        //TODO: work with fuzzy variables
        //TODO: work with possible redefinitions in THEN expressions
        public static SemanticResult makeSemanticAnalysis(ParserResult parserResult, List<IntVariable> variables)
        {
            SemanticResult semanticResult = new SemanticResult();
            semanticResult.isCorrect = true;
            semanticResult.output = new List<string>();

            foreach(Lexeme lexeme in parserResult.lexemesList)
            {
                if(LexemeTypes.IsIdentifier(lexeme.lexemeType))
                {
                    bool permittedIdentifier = false;
                    foreach (IntVariable v in variables)
                    {
                        if (v.name.Equals(lexeme.lexemeText))
                        {
                            permittedIdentifier = true;
                            break;
                        }
                    }
                    if(!permittedIdentifier)
                    {
                        semanticResult.output.Add("Неизвестный идентификатор: " + lexeme.lexemeText + "\n");
                        semanticResult.isCorrect = false;
                    }
                }
            }

            return semanticResult;
        }
    }
}
