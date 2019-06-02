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
            //Lexemes now stored as Identifier-Assign-(Value/Identifier)-Comma-Identifier-...
            Lexeme identifierSavior = null;
            for (int i = 0; i < parserResult.Count; i++)
            {
                Lexeme lex = parserResult[i];
                if (LexemeTypes.IsIdentifier(lex.lexemeType))
                {
                    if ((i != 0) && (parserResult[i - 1].lexemeType == LexemeType.Assign))
                    {
                        //В данном случае lex - это переменная ПОСЛЕ знака "="

                        //Пытаемся получить переменную ПОСЛЕ знака "=" как целочисленную
                        IntViewVariable intAfterVariable = getIntVariableByLexeme(lex, variables);
                        if (intAfterVariable != null)
                        {
                            bool found = false;
                            //Пытаемся получить переменную ДО знака "=" как целочисленную
                            foreach (IntViewVariable iv in variables.intVariables)
                            {
                                //Если нашли, ей нужно установить значение
                                if (iv.name.Equals(identifierSavior.lexemeText))
                                {
                                    iv.value = intAfterVariable.value;
                                    if (iv.firstReassignmentLevel == -1)
                                    {
                                        iv.firstReassignmentLevel = subexpressionLevel;
                                    }
                                    found = true;
                                    break;
                                }
                            }

                            //Если всё ещё не нашли, то...
                            if (!found)
                            {
                                //...пытаемся получить переменную ДО знака "=" как нечеткую
                                int levelSavior = -1;
                                foreach (FuzzyViewVariable fv in variables.fuzzyVariables)
                                {
                                    //Если нашли, её удаляем...
                                    if (fv.name.Equals(identifierSavior.lexemeText))
                                    {
                                        levelSavior = fv.firstReassignmentLevel;
                                        variables.fuzzyVariables.Remove(fv);
                                        found = true;
                                        break;
                                    }
                                }
                                //...и добавляем новую целочисленную
                                if (found)
                                {
                                    variables.intVariables.Add(new IntViewVariable(identifierSavior.lexemeText, intAfterVariable.value, levelSavior));
                                }
                            }

                            if(!found)
                            {
                                variables.intVariables.Add(new IntViewVariable(identifierSavior.lexemeText, intAfterVariable.value));
                            }
                        }
                        else
                        {
                            //Пытаемся получить переменную ПОСЛЕ знака "=" как нечеткую
                            FuzzyViewVariable fuzzyAfterVariable = getFuzzyVariableByLexeme(lex, variables);
                            if(fuzzyAfterVariable != null)
                            {
                                bool found = false;
                                //Пытаемся получить переменную ДО знака "=" как нечеткую
                                foreach (FuzzyViewVariable fv in variables.fuzzyVariables)
                                {
                                    //Если нашли, ей нужно установить значение
                                    if (fv.name.Equals(identifierSavior.lexemeText))
                                    {
                                        fv.value = fuzzyAfterVariable.value;
                                        if (fv.firstReassignmentLevel == -1)
                                        {
                                            fv.firstReassignmentLevel = subexpressionLevel;
                                        }
                                        found = true;
                                        break;
                                    }
                                }

                                //Если всё ещё не нашли, то...
                                if (!found)
                                {
                                    //...пытаемся получить переменную ДО знака "=" как целочисленную
                                    int levelSavior = -1;
                                    foreach (IntViewVariable iv in variables.intVariables)
                                    {
                                        //Если нашли, её удаляем...
                                        if (iv.name.Equals(identifierSavior.lexemeText))
                                        {
                                            variables.intVariables.Remove(iv);
                                            found = true;
                                            break;
                                        }
                                    }
                                    //...и добавляем новую нечеткую
                                    if (found)
                                    {
                                        variables.fuzzyVariables.Add(new FuzzyViewVariable(identifierSavior.lexemeText, fuzzyAfterVariable.value, levelSavior));
                                    }
                                }
                                
                                if(!found)
                                {
                                    variables.fuzzyVariables.Add(new FuzzyViewVariable(identifierSavior.lexemeText, fuzzyAfterVariable.value));
                                }
                            }
                        }
                        identifierSavior = null;
                    }
                    else
                    {
                        //В данном случае мы только прочитали первый идентификатор
                        identifierSavior = lex;
                    }
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
        /// Метод получения представления целочисленной переменной из лексемы.
        /// Возвращает представление целочисленной переменной <see cref="IntViewVariable"/>
        /// </summary>
        /// <param name="lexeme">Лексема</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static IntViewVariable getIntVariableByLexeme(Lexeme lexeme, VariablesStorage variablesStorage)
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

            return null;
        }

        /// <summary>
        /// Метод получения представления нечекой переменной из лексемы.
        /// Возвращает представление нечеткой переменной <see cref="FuzzyViewVariable"/>
        /// </summary>
        /// <param name="lexeme">Лексема</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static FuzzyViewVariable getFuzzyVariableByLexeme(Lexeme lexeme, VariablesStorage variablesStorage)
        {
            if (lexeme == null || !LexemeTypes.IsIdentifier(lexeme.lexemeType))
            {
                return null;
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
