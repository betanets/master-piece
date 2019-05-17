using master_piece.service.fuzzy_variable;
using master_piece.service.init_variables;
using master_piece.service.parser;
using SQLite;
using System;
using System.Collections.Generic;
using static master_piece.service.subexpression.Operation;

namespace master_piece.service.subexpression
{
    /// <summary>
    /// Сервис по работе с подвыражениями
    /// </summary>
    class SubexpressionService
    {
        /// <summary>
        /// Объект соединения с базой данных
        /// </summary>
        private SQLiteConnection dbConnection;

        /// <summary>
        /// Сервис по работе с нечеткими переменными 
        /// </summary>
        private FuzzyVariableService fuzzyVariableService;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="arg_dbConnection">Объект соединения с базой данных</param>
        public SubexpressionService(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
            fuzzyVariableService = new FuzzyVariableService(dbConnection);
        }

        /// <summary>
        /// Метод создания списка подвыражений из списка лексем
        /// </summary>
        /// <param name="lexemes">Список лексем</param>
        /// <param name="expressionLevel">Порядковый номер выражения</param>
        public List<Subexpression> createSubexpressionsList(List<Lexeme> lexemes, int expressionLevel)
        {
            List<Subexpression> subexpressions = new List<Subexpression>();
            Stack<Subexpression> subexpressionsStack = new Stack<Subexpression>();
            Stack<Lexeme> lexemesStack = new Stack<Lexeme>();

            foreach (Lexeme lex in lexemes)
            {
                if (LexemeTypes.IsValue(lex.lexemeType) || LexemeTypes.IsIdentifier(lex.lexemeType))
                {
                    lexemesStack.Push(lex);
                }
                else if (LexemeTypes.IsOperation(lex.lexemeType))
                {
                    if (lexemesStack.Count >= 2)
                    {
                        Lexeme lexemeSecond = lexemesStack.Pop();
                        Lexeme lexemeFirst = lexemesStack.Pop();
                        Subexpression subexpression = new Subexpression(lexemeFirst, getOperationByLexeme(lex.lexemeText), lexemeSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                    else if (subexpressionsStack.Count >= 2)
                    {
                        Subexpression subexpressionSecond = subexpressionsStack.Pop();
                        Subexpression subexpressionFirst = subexpressionsStack.Pop();
                        Subexpression subexpression = new Subexpression(subexpressionFirst, getOperationByLexeme(lex.lexemeText), subexpressionSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                    else if(lexemesStack.Count == 1 && subexpressionsStack.Count == 1)
                    {
                        Subexpression subexpressionFirst = subexpressionsStack.Pop();
                        Lexeme lexemeSecond = lexemesStack.Pop();
                        Subexpression subexpression = new Subexpression(subexpressionFirst, getOperationByLexeme(lex.lexemeText), lexemeSecond, expressionLevel);
                        subexpressions.Add(subexpression);
                        subexpressionsStack.Push(subexpression);
                    }
                }
            }

            //Last expression in list is always major
            if (subexpressions.Count > 0)
            {
                subexpressions[subexpressions.Count - 1].major = true;
            }

            return subexpressions;
        }

        /// <summary>
        /// Метод вычисления дубликатов подвыражений
        /// </summary>
        /// <param name="subexpressions">Список подвыражений</param>
        /// <param name="variables">Хранилище переменных</param>
        public void calculateDuplicates(List<Subexpression> subexpressions, VariablesStorage variables)
        {
            foreach (Subexpression subexpression in subexpressions)
            {
                if (subexpression.mustBePrecalculated)
                {
                    subexpression.value = calculateSubexpressionValue(subexpression, variables);
                }
            }
        }

        /// <summary>
        /// Метод вычисления одного подвыражения.
        /// Возвращает значение подвыражения - true или false
        /// </summary>
        /// <param name="subexpression">Подвыражение</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public bool calculateSubexpressionValue(Subexpression subexpression, VariablesStorage variablesStorage)
        {
            //If subexpression already has the value, simply return it
            if (subexpression.value.HasValue) return subexpression.value.Value;

            if (subexpression.isLeaf())
            {
                int? intValueFirst = null, intValueSecond = null;
                string stringValueFirst = null, stringValueSecond = null;

                //Checking first lexeme in subexpression
                if (LexemeTypes.IsIntValue(subexpression.lexemeFirst.lexemeType))
                {
                    intValueFirst = Convert.ToInt32(subexpression.lexemeFirst.lexemeText);
                }
                else if (LexemeTypes.IsFuzzyValue(subexpression.lexemeFirst.lexemeType))
                {
                    stringValueFirst = subexpression.lexemeFirst.lexemeText;
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeFirst.lexemeType))
                {
                    var obj = getValueFromVariablesStorage(subexpression.lexemeFirst.lexemeText, variablesStorage);
                    if ((obj == null) || (obj is int?))
                    {
                        intValueFirst = Convert.ToInt32(obj);
                    }
                    else
                    {
                        stringValueFirst = Convert.ToString(obj);
                    }  
                }

                //Checking second lexeme in subexpression
                if (LexemeTypes.IsIntValue(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = Convert.ToInt32(subexpression.lexemeSecond.lexemeText);
                }
                else if (LexemeTypes.IsFuzzyValue(subexpression.lexemeSecond.lexemeType))
                {
                    stringValueSecond = subexpression.lexemeSecond.lexemeText;
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeSecond.lexemeType))
                {
                    var obj = getValueFromVariablesStorage(subexpression.lexemeSecond.lexemeText, variablesStorage);
                    if ((obj == null) || (obj is int?))
                    {
                        intValueSecond = Convert.ToInt32(obj);
                    }
                    else
                    {
                        stringValueSecond = Convert.ToString(obj);
                    }
                }

                //If subexpression has uninitialized variables, we couldn't evaluate it. Return false in this case
                if((!intValueFirst.HasValue && stringValueFirst == null) || (!intValueSecond.HasValue && stringValueSecond == null))
                {
                    return false;
                }

                switch(subexpression.operation)
                {
                    //It is not OK but possible to use AND and OR operaions in leaf expression
                    case OperationEnum.And:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return (intValueFirst.Value != 0) && (intValueSecond.Value != 0);
                            }
                            else if (stringValueSecond != null)
                            {
                                return (intValueFirst.Value != 0) && (stringValueSecond != null);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return (stringValueFirst != null) && (intValueSecond.Value != 0);
                            }
                            else if (stringValueSecond != null)
                            {
                                return (stringValueFirst != null) && (stringValueSecond != null);
                            }
                        }
                        return false;
                    case OperationEnum.Or:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return (intValueFirst.Value != 0) || (intValueSecond.Value != 0);
                            }
                            else if (stringValueSecond != null)
                            {
                                return (intValueFirst.Value != 0) || (stringValueSecond != null);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return (stringValueFirst != null) || (intValueSecond.Value != 0);
                            }
                            else if (stringValueSecond != null)
                            {
                                return (stringValueFirst != null) || (stringValueSecond != null);
                            }
                        }
                        return false;
                    //Comparison operations are fully OK
                    case OperationEnum.Equal:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value == intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyEquals(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyEquals(intValueSecond.Value, stringValueFirst);
                            }
                            else if (stringValueSecond != null)
                            {
                                return stringValueFirst == stringValueSecond;
                            }
                        }
                        return false;
                    case OperationEnum.NotEqual:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value != intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyNotEquals(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyNotEquals(intValueSecond.Value, stringValueFirst);
                            }
                            else if (stringValueSecond != null)
                            {
                                return stringValueFirst != stringValueSecond;
                            }
                        }
                        return false;
                    case OperationEnum.More:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value > intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMore(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMore(stringValueFirst, intValueSecond.Value);
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMore(stringValueFirst, stringValueSecond);
                            }
                        }
                        return false;
                    case OperationEnum.MoreOrEqual:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value >= intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMoreOrEquals(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMoreOrEquals(stringValueFirst, intValueSecond.Value);
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyMoreOrEquals(stringValueFirst, stringValueSecond);
                            }
                        }
                        return false;
                    case OperationEnum.Less:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value < intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLess(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLess(stringValueFirst, intValueSecond.Value);
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLess(stringValueFirst, stringValueSecond);
                            }
                        }
                        return false;
                    case OperationEnum.LessOrEqual:
                        if (intValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return intValueFirst.Value <= intValueSecond.Value;
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLessOrEquals(intValueFirst.Value, stringValueSecond);
                            }
                        }
                        else if (stringValueFirst != null)
                        {
                            if (intValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLessOrEquals(stringValueFirst, intValueSecond.Value);
                            }
                            else if (stringValueSecond != null)
                            {
                                return fuzzyVariableService.fuzzyLessOrEquals(stringValueFirst, stringValueSecond);
                            }
                        }
                        return false;
                    //Rest operations are not allowed, and in this case false will be returned
                    default:
                        return false;
                }
            }
            else if (subexpression.isMixed())
            {
                bool? preBooster = calculationBooster(subexpression);
                if (preBooster.HasValue)
                {
                    return preBooster.Value;
                }

                //Calculate first subexpression value if it doesn't exists
                if (!subexpression.subexpressionFirst.value.HasValue)
                {
                    subexpression.subexpressionFirst.value = calculateSubexpressionValue(subexpression.subexpressionFirst, variablesStorage);
                }

                bool? secondBooster = calculationBooster(subexpression);
                if (secondBooster.HasValue)
                {
                    return secondBooster.Value;
                }

                int? intValueSecond = null;
                string stringValueSecond = null;

                //Checking second lexeme in subexpression
                if (LexemeTypes.IsIntValue(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = Convert.ToInt32(subexpression.lexemeSecond.lexemeText);
                }
                else if (LexemeTypes.IsFuzzyValue(subexpression.lexemeSecond.lexemeType))
                {
                    stringValueSecond = subexpression.lexemeSecond.lexemeText;
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeSecond.lexemeType))
                {
                    var obj = getValueFromVariablesStorage(subexpression.lexemeSecond.lexemeText, variablesStorage);
                    if ((obj == null) || (obj is int?))
                    {
                        intValueSecond = Convert.ToInt32(obj);
                    }
                    else
                    {
                        stringValueSecond = Convert.ToString(obj);
                    }
                }

                //If subexpression has uninitialized variables, we couldn't evaluate it. Return false in this case
                if (!intValueSecond.HasValue && stringValueSecond == null)
                {
                    return false;
                }

                switch (subexpression.operation)
                {
                    //AND and OR operaions are fully OK in mixed subexpression
                    case OperationEnum.And:
                        if (intValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value && (intValueSecond.Value != 0);
                        }
                        else if(stringValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value && (stringValueSecond != null);
                        }
                        else
                        {
                            return false;
                        }
                    case OperationEnum.Or:
                        if (intValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value || (intValueSecond.Value != 0);
                        }
                        else if (stringValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value || (stringValueSecond != null);
                        }
                        else
                        {
                            return false;
                        }
                    //Comparison operations are strange but possible
                    case OperationEnum.Equal:
                        if (intValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value == (intValueSecond.Value != 0);
                        }
                        else if (stringValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value == (stringValueSecond != null);
                        }
                        else
                        {
                            return false;
                        }
                    case OperationEnum.NotEqual:
                        if (intValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value != (intValueSecond.Value != 0);
                        }
                        else if (stringValueSecond != null)
                        {
                            return subexpression.subexpressionFirst.value.Value != (stringValueSecond != null);
                        }
                        else
                        {
                            return false;
                        }
                    //Rest operations are not allowed, and in this case false will be returned
                    default:
                        return false;
                }
            }
            //Ordinary subexpression are here
            else
            {
                bool? preBooster = calculationBooster(subexpression);
                if (preBooster.HasValue)
                {
                    return preBooster.Value;
                }

                //Calculate first subexpression value if it doesn't exists
                if (!subexpression.subexpressionFirst.value.HasValue)
                {
                    subexpression.subexpressionFirst.value = calculateSubexpressionValue(subexpression.subexpressionFirst, variablesStorage);
                }

                bool? secondBooster = calculationBooster(subexpression);
                if (secondBooster.HasValue)
                {
                    return secondBooster.Value;
                }

                //Calculate second subexpression value if it doesn't exists
                if (!subexpression.subexpressionSecond.value.HasValue)
                {
                    subexpression.subexpressionSecond.value = calculateSubexpressionValue(subexpression.subexpressionSecond, variablesStorage);
                }

                switch (subexpression.operation)
                {
                    //AND and OR operaions are fully OK in mixed subexpression
                    case OperationEnum.And:
                        return subexpression.subexpressionFirst.value.Value && subexpression.subexpressionSecond.value.Value;
                    case OperationEnum.Or:
                        return subexpression.subexpressionFirst.value.Value || subexpression.subexpressionSecond.value.Value;
                    //Comparison operations are strange but possible
                    case OperationEnum.Equal:
                        return subexpression.subexpressionFirst.value.Value == subexpression.subexpressionSecond.value.Value;
                    case OperationEnum.NotEqual:
                        return subexpression.subexpressionFirst.value.Value != subexpression.subexpressionSecond.value.Value;
                    //Rest operations are not allowed, and in this case false will be returned
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Метод поиска подвыражений, не влияющих на общий результат.
        /// Возвращает null, если подвыражение следует считать в общем потоке, и true или false (как значения подвыражения), если удалось отбросить не влияющую на результат часть
        /// </summary>
        /// <param name="subexpression">Подвыражение</param>
        public bool? calculationBooster(Subexpression subexpression)
        {
            if (subexpression.subexpressionFirst.value.HasValue)
            {
                if (subexpression.operation == OperationEnum.And)
                {
                    if (subexpression.subexpressionFirst.value.Value.Equals(false))
                    {
                        return false;
                    }
                }
                else if (subexpression.operation == OperationEnum.Or)
                {
                    if (subexpression.subexpressionFirst.value.Value.Equals(true))
                    {
                        return true;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Метод получения значения представления переменной по алиасу
        /// </summary>
        /// <param name="identifier">Алиас переменной</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        //TODO: move to another class?
        public object getValueFromVariablesStorage(string identifier, VariablesStorage variablesStorage)
        {
            foreach (IntViewVariable iv in variablesStorage.intVariables)
            {
                if (iv.name.Equals(identifier))
                {
                    return iv.value;
                }
            }

            foreach (FuzzyViewVariable iv in variablesStorage.fuzzyVariables)
            {
                if (iv.name.Equals(identifier))
                {
                    return iv.value;
                }
            }

            return null;
        }
    }
}
