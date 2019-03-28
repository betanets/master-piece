using master_piece.lexeme;
using master_piece.variable;
using System;
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

        //TODO: work with fuzzy variables
        //TODO: possible move to another or separate sevice
        public static void assignVariables(ParserResult parserResult, List<IntVariable> intVariableStorage, int subexpressionLevel)
        {
            //List<IntVariable> intVariables = new List<IntVariable>();

            //Lexemes now stored as Identifier-Assign-Value-Comma-Identifier-...
            //TODO: check assign and comma lexemes
            Lexeme identifierSavior = null;
            foreach (Lexeme lex in parserResult.lexemesList)
            {
                if (LexemeTypes.IsIdentifier(lex.lexemeType))
                {
                    identifierSavior = lex;
                }
                else if (LexemeTypes.IsValue(lex.lexemeType))
                {
                    if(identifierSavior != null)
                    {
                        //TODO: move to helper method
                        bool assignedToExistingVariable = false;
                        foreach (IntVariable iv in intVariableStorage)
                        {
                            if (iv.name.Equals(identifierSavior.lexemeText))
                            {
                                iv.value = Convert.ToInt32(lex.lexemeText);
                                //Setting reassignment level to avoid marking some expressions as duplicates
                                if(iv.firstReassignmentLevel == -1)
                                {
                                    iv.firstReassignmentLevel = subexpressionLevel;
                                }
                                assignedToExistingVariable = true;
                                break;
                            }
                        }
                        if (!assignedToExistingVariable)
                        {
                            intVariableStorage.Add(new IntVariable(identifierSavior.lexemeText, Convert.ToInt32(lex.lexemeText)));
                        }
                    }
                }
            }
        }

        public static IntVariable getIntVariableByLexeme(Lexeme lexeme, List<IntVariable> intVariableStorage)
        {
            if(lexeme == null || !LexemeTypes.IsIdentifier(lexeme.lexemeType))
            {
                return null;
            }

            foreach(IntVariable iv in intVariableStorage)
            {
                if (iv.name.Equals(lexeme.lexemeText))
                {
                    return iv;
                }
            }
            return null;
        }

        public static List<IntVariable> getIntVariablesBySubexpression(Subexpression subexpression, List<IntVariable> intVariableStorage)
        {
            List<IntVariable> intVariables = new List<IntVariable>();
            if (subexpression.isLeaf())
            {
                IntVariable ivFirst = getIntVariableByLexeme(subexpression.lexemeFirst, intVariableStorage);
                if (ivFirst != null)
                {
                    intVariables.Add(ivFirst);
                }
                IntVariable ivSecond = getIntVariableByLexeme(subexpression.lexemeSecond, intVariableStorage);
                if (ivSecond != null)
                {
                    intVariables.Add(ivSecond);
                }
            }
            else if (subexpression.isMixed())
            {
                intVariables.AddRange(getIntVariablesBySubexpression(subexpression.subexpressionFirst, intVariableStorage));
                IntVariable ivSecond = getIntVariableByLexeme(subexpression.lexemeSecond, intVariableStorage);
                if (ivSecond != null)
                {
                    intVariables.Add(ivSecond);
                }
            }
            else
            {
                intVariables.AddRange(getIntVariablesBySubexpression(subexpression.subexpressionFirst, intVariableStorage));
                intVariables.AddRange(getIntVariablesBySubexpression(subexpression.subexpressionSecond, intVariableStorage));
            }

            return intVariables;
        }
    }
}
