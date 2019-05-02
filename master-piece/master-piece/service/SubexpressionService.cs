using master_piece.lexeme;
using master_piece.variable;
using System;
using System.Collections.Generic;
using static master_piece.service.Operation;

namespace master_piece.service
{
    class SubexpressionService
    {
        public static List<Subexpression> createSubexpressionsList(List<Lexeme> lexemes, int expressionLevel)
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
        /// Duplicates precalculation method
        /// </summary>
        /// <param name="subexpressions">List of subexpressions</param>
        /// <param name="intVariables">List of currently initialized variables</param>
        public static void calculateDuplicates(List<Subexpression> subexpressions, List<IntVariable> intVariables)
        {
            foreach (Subexpression subexpression in subexpressions)
            {
                if (subexpression.mustBePrecalculated)
                {
                    subexpression.value = calculateSubexpressionValue(subexpression, intVariables);
                }
            }
        }

        //TODO: work with fuzzy values
        public static bool calculateSubexpressionValue(Subexpression subexpression, List<IntVariable> intVariables)
        {
            //If subexpression already has the value, simply return it
            if (subexpression.value.HasValue) return subexpression.value.Value;

            if (subexpression.isLeaf())
            {
                int? intValueFirst = null, intValueSecond = null;
                //Checking first lexeme in subexpression
                if (LexemeTypes.IsValue(subexpression.lexemeFirst.lexemeType))
                {
                    intValueFirst = Convert.ToInt32(subexpression.lexemeFirst.lexemeText);
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeFirst.lexemeType))
                {
                    intValueFirst = getValueFromIntVariablesStorage(subexpression.lexemeFirst.lexemeText, intVariables);
                }

                //Checking second lexeme in subexpression
                if (LexemeTypes.IsValue(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = Convert.ToInt32(subexpression.lexemeSecond.lexemeText);
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = getValueFromIntVariablesStorage(subexpression.lexemeSecond.lexemeText, intVariables);
                }

                //If subexpression has uninitialized variables, we couldn't evaluate it. Return false in this case
                //TODO: is it correct to return false in this case?
                if(!intValueFirst.HasValue || !intValueSecond.HasValue)
                {
                    return false;
                }

                switch(subexpression.operation)
                {
                    //It is not OK but possible to use AND and OR operaions in leaf expression
                    case OperationEnum.And:
                        return (intValueFirst.Value != 0) && (intValueSecond.Value != 0);
                    case OperationEnum.Or:
                        return (intValueFirst.Value != 0) || (intValueSecond.Value != 0);
                    //Comparison operations are fully OK
                    case OperationEnum.Equal:
                        return intValueFirst.Value == intValueSecond.Value;
                    case OperationEnum.NotEqual:
                        return intValueFirst.Value != intValueSecond.Value;
                    case OperationEnum.More:
                        return intValueFirst.Value > intValueSecond.Value;
                    case OperationEnum.MoreOrEqual:
                        return intValueFirst.Value >= intValueSecond.Value;
                    case OperationEnum.Less:
                        return intValueFirst.Value < intValueSecond.Value;
                    case OperationEnum.LessOrEqual:
                        return intValueFirst.Value <= intValueSecond.Value;
                    //Rest operations are not allowed, and in this case false will be returned
                    default:
                        return false;
                }
            }
            else if (subexpression.isMixed())
            {
                //Calculate first subexpression value if it doesn't exists
                if(!subexpression.subexpressionFirst.value.HasValue)
                {
                    subexpression.subexpressionFirst.value = calculateSubexpressionValue(subexpression.subexpressionFirst, intVariables);
                }

                int? intValueSecond = null;
                //Checking second lexeme in subexpression
                if (LexemeTypes.IsValue(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = Convert.ToInt32(subexpression.lexemeSecond.lexemeText);
                }
                else if (LexemeTypes.IsIdentifier(subexpression.lexemeSecond.lexemeType))
                {
                    intValueSecond = getValueFromIntVariablesStorage(subexpression.lexemeSecond.lexemeText, intVariables);
                }

                //If subexpression has uninitialized variables, we couldn't evaluate it. Return false in this case
                //TODO: is it correct to return false in this case?
                if (!intValueSecond.HasValue)
                {
                    return false;
                }

                switch (subexpression.operation)
                {
                    //AND and OR operaions are fully OK in mixed subexpression
                    case OperationEnum.And:
                        return subexpression.subexpressionFirst.value.Value && (intValueSecond.Value != 0);
                    case OperationEnum.Or:
                        return subexpression.subexpressionFirst.value.Value || (intValueSecond.Value != 0);
                    //Comparison operations are strange but possible
                    case OperationEnum.Equal:
                        return subexpression.subexpressionFirst.value.Value == (intValueSecond.Value != 0);
                    case OperationEnum.NotEqual:
                        return subexpression.subexpressionFirst.value.Value != (intValueSecond.Value != 0);
                    //Rest operations are not allowed, and in this case false will be returned
                    default:
                        return false;
                }
            }
            //Ordinary subexpression are here
            //TODO: add calculation booster block
            else
            {
                //Calculate first subexpression value if it doesn't exists
                if (!subexpression.subexpressionFirst.value.HasValue)
                {
                    subexpression.subexpressionFirst.value = calculateSubexpressionValue(subexpression.subexpressionFirst, intVariables);
                }

                //Calculate second subexpression value if it doesn't exists
                if (!subexpression.subexpressionSecond.value.HasValue)
                {
                    subexpression.subexpressionSecond.value = calculateSubexpressionValue(subexpression.subexpressionSecond, intVariables);
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

        //TODO: move to another class?
        public static int? getValueFromIntVariablesStorage(string identifier, List<IntVariable> intVariableStorage)
        {
            foreach (IntVariable iv in intVariableStorage)
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
