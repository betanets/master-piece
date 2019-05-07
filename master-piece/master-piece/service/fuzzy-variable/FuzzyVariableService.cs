using master_piece.domain;
using master_piece.lexeme;
using master_piece.service.init_variables;
using master_piece.variable;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace master_piece.service.fuzzy_variable
{
    class FuzzyVariableService
    {
        private SQLiteConnection dbConnection;

        public FuzzyVariableService(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
        }

        public FuzzyVariable getFuzzyVariableById(int? fuzzyVariableId)
        {
            if (!fuzzyVariableId.HasValue)
            {
                return null;
            }
            else
            {
                List<FuzzyVariable> variables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where id = ? and deleted = '0' limit 1", fuzzyVariableId);
                if(variables.Count == 1)
                {
                    return variables.ElementAt(0);
                }
                else
                {
                    return null;
                }
            }
        }

        public int? getFuzzyVariableIdByName(string fuzzyVariableName)
        {
            fuzzyVariableName = fuzzyVariableName.Trim(' ', '\t', '"');
            List<FuzzyVariable> variables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where name = ? and deleted = '0' limit 1", fuzzyVariableName);
            if (variables.Count == 1)
            {
                return variables.ElementAt(0).id;
            }
            else
            {
                return null;
            }
        }

        public FuzzyVariableSelectionResult makeSelection(VariablesStorage variables)
        {
            foreach(FuzzyViewVariable fvv in variables.fuzzyVariables)
            {
                if(fvv.fuzzyVariableId.HasValue)
                {
                    continue;
                }
                fvv.fuzzyVariableId = getFuzzyVariableIdByName(fvv.value);
                if(!fvv.fuzzyVariableId.HasValue)
                {
                    return new FuzzyVariableSelectionResult(false, "Не найдена переменная с нечетким значением: " + fvv.value);
                }
            }
            return new FuzzyVariableSelectionResult(true, null);
        }

        public FuzzyVariableSelectionResult makeSelection(List<Lexeme> lexemes)
        {
            foreach(Lexeme lex in lexemes)
            {
                if(lex.lexemeType.Equals(LexemeType.FuzzyValue))
                {
                    int? fvId = getFuzzyVariableIdByName(lex.lexemeText);
                    if(!fvId.HasValue)
                    {
                        return new FuzzyVariableSelectionResult(false, "Не найдена переменная с нечетким значением: " + lex.lexemeText);
                    }
                }
            }
            return new FuzzyVariableSelectionResult(true, null);
        }
    }
}
