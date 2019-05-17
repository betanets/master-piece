using master_piece.service.init_variables;
using master_piece.service.parser;
using master_piece.service.subexpression;
using System;
using System.Collections.Generic;

namespace master_piece.service.lexical_analysis
{
    /// <summary>
    /// Сервис лексического анализа лексем
    /// </summary>
    class LexicalAnalysisService
    {
        /// <summary>
        /// Метод выполнения лексического анализа.
        /// Возвращает результат лексического анализа <see cref="LexicalAnalysisResult"/>
        /// </summary>
        /// <param name="parserResult">Список лексем</param>
        /// <param name="variables">Хранилище переменных</param>
        //TODO: work with possible redefinitions in THEN expressions
        public static LexicalAnalysisResult makeSemanticAnalysis(List<Lexeme> parserResult, VariablesStorage variables)
        {
            LexicalAnalysisResult semanticResult = new LexicalAnalysisResult();
            semanticResult.isCorrect = true;
            semanticResult.output = new List<string>();

            foreach(Lexeme lexeme in parserResult)
            {
                if(LexemeTypes.IsIdentifier(lexeme.lexemeType))
                {
                    bool permittedIdentifier = false;
                    //Searching identifier in int variables...
                    foreach (IntViewVariable v in variables.intVariables)
                    {
                        if (v.name.Equals(lexeme.lexemeText))
                        {
                            permittedIdentifier = true;
                            break;
                        }
                    }
                    //Searching identifier in fuzzy variables...
                    if (!permittedIdentifier)
                    {
                        foreach (FuzzyViewVariable v in variables.fuzzyVariables)
                        {
                            if (v.name.Equals(lexeme.lexemeText))
                            {
                                permittedIdentifier = true;
                                break;
                            }
                        }
                    }
                    if (!permittedIdentifier)
                    {
                        semanticResult.output.Add("Неизвестный идентификатор: " + lexeme.lexemeText + "\n");
                        semanticResult.isCorrect = false;
                    }
                }
            }

            return semanticResult;
        }

        /// <summary>
        /// Метод присваивания переменных.
        /// Изменяет значения в хранилище переменных.
        /// Выполняется для выражений ТО или ИНАЧЕ, а также после обработки каждого из нечетких правил.
        /// </summary>
        /// <param name="parserResult">Список лексем</param>
        /// <param name="variables">Хранилище переменных</param>
        /// <param name="subexpressionLevel">Уровень текущего подвыражения</param>
        //TODO: possible move to another or separate sevice
        //TODO: split into two methods: one for assign value, another for assign level
        public static void assignVariables(List<Lexeme> parserResult, VariablesStorage variables, int subexpressionLevel)
        {
            //Lexemes now stored as Identifier-Assign-Value-Comma-Identifier-...
            //TODO: check assign and comma lexemes
            Lexeme identifierSavior = null;
            foreach (Lexeme lex in parserResult)
            {
                if (LexemeTypes.IsIdentifier(lex.lexemeType))
                {
                    identifierSavior = lex;
                }
                else if (LexemeTypes.IsIntValue(lex.lexemeType))
                {
                    if (identifierSavior != null)
                    {
                        //TODO: move to helper method
                        bool assignedToExistingVariable = false;
                        foreach (IntViewVariable iv in variables.intVariables)
                        {
                            if (iv.name.Equals(identifierSavior.lexemeText))
                            {
                                iv.value = Convert.ToInt32(lex.lexemeText);
                                //Setting reassignment level to avoid marking some expressions as duplicates
                                if (iv.firstReassignmentLevel == -1)
                                {
                                    iv.firstReassignmentLevel = subexpressionLevel;
                                }
                                assignedToExistingVariable = true;
                                break;
                            }
                        }
                        if (!assignedToExistingVariable)
                        {
                            variables.intVariables.Add(new IntViewVariable(identifierSavior.lexemeText, Convert.ToInt32(lex.lexemeText)));
                        }
                        identifierSavior = null;
                    }
                }
                else if (LexemeTypes.IsFuzzyValue(lex.lexemeType))
                {
                    //TODO: move to helper method
                    bool assignedToExistingVariable = false;
                    foreach (FuzzyViewVariable iv in variables.fuzzyVariables)
                    {
                        if (iv.name.Equals(identifierSavior.lexemeText))
                        {
                            iv.value = lex.lexemeText;
                            //Setting reassignment level to avoid marking some expressions as duplicates
                            if (iv.firstReassignmentLevel == -1)
                            {
                                iv.firstReassignmentLevel = subexpressionLevel;
                            }
                            assignedToExistingVariable = true;
                            break;
                        }
                    }
                    if (!assignedToExistingVariable)
                    {
                        variables.fuzzyVariables.Add(new FuzzyViewVariable(identifierSavior.lexemeText, lex.lexemeText));
                    }
                    identifierSavior = null;
                }
            }
        }

        /// <summary>
        /// Метод получения абстрактного представления переменной из лексемы.
        /// Возврщает абстрактное представление целочисленной или нечеткой переменной в алгоритме <see cref="AbstractViewVariable"/>
        /// </summary>
        /// <param name="lexeme">Лексема</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static AbstractViewVariable getVariableByLexeme(Lexeme lexeme, VariablesStorage variablesStorage)
        {
            if (lexeme == null || !LexemeTypes.IsIdentifier(lexeme.lexemeType))
            {
                return null;
            }

            foreach (IntViewVariable iv in variablesStorage.intVariables)
            {
                if (iv.name.Equals(lexeme.lexemeText))
                {
                    return iv;
                }
            }

            foreach (FuzzyViewVariable iv in variablesStorage.fuzzyVariables)
            {
                if (iv.name.Equals(lexeme.lexemeText))
                {
                    return iv;
                }
            }
            return null;
        }

        /// <summary>
        /// Метод получения списка абстрактных представлений переменных из подвыражения.
        /// Возврщает список абстрактных представлений переменных <see cref="AbstractViewVariable"/>
        /// </summary>
        /// <param name="subexpression">Подвыражение</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static List<AbstractViewVariable> getVariablesBySubexpression(Subexpression subexpression, VariablesStorage variablesStorage)
        {
            List<AbstractViewVariable> returnVariables = new List<AbstractViewVariable>();
            if (subexpression.isLeaf())
            {
                AbstractViewVariable ivFirst = getVariableByLexeme(subexpression.lexemeFirst, variablesStorage);
                if (ivFirst != null)
                {
                    returnVariables.Add(ivFirst);
                }
                AbstractViewVariable ivSecond = getVariableByLexeme(subexpression.lexemeSecond, variablesStorage);
                if (ivSecond != null)
                {
                    returnVariables.Add(ivSecond);
                }
            }
            else if (subexpression.isMixed())
            {
                returnVariables.AddRange(getVariablesBySubexpression(subexpression.subexpressionFirst, variablesStorage));
                AbstractViewVariable ivSecond = getVariableByLexeme(subexpression.lexemeSecond, variablesStorage);
                if (ivSecond != null)
                {
                    returnVariables.Add(ivSecond);
                }
            }
            else
            {
                returnVariables.AddRange(getVariablesBySubexpression(subexpression.subexpressionFirst, variablesStorage));
                returnVariables.AddRange(getVariablesBySubexpression(subexpression.subexpressionSecond, variablesStorage));
            }

            return returnVariables;
        }
    }
}
