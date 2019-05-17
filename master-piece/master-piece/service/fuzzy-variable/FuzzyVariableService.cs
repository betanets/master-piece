using master_piece.domain;
using master_piece.service.init_variables;
using master_piece.service.parser;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace master_piece.service.fuzzy_variable
{
    /// <summary>
    /// Сервис по работе с нечеткими переменными
    /// </summary>
    class FuzzyVariableService
    {
        /// <summary>
        /// Объект соединения с базой данных
        /// </summary>
        private SQLiteConnection dbConnection;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="arg_dbConnection">Объект соединения с базой данных</param>
        public FuzzyVariableService(SQLiteConnection arg_dbConnection)
        {
            dbConnection = arg_dbConnection;
        }

        /// <summary>
        /// Метод получения сущности нечекой переменной по ID
        /// </summary>
        /// <param name="fuzzyVariableId">ID нечеткой переменной. Может быть null</param>
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

        /// <summary>
        /// Метод получения ID нечеткой переменной по алиасу.
        /// Если алиасу соответствуют несколько нечетких переменных, или не соответствует ни одна, то вернется null.
        /// </summary>
        /// <param name="fuzzyVariableName">Алиас нечеткой переменной</param>
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

        /// <summary>
        /// Метод получения сущности нечеткой переменной по алиасу.
        /// Если алиасу соответствуют несколько нечетких переменных, или не соответствует ни одна, то вернется null.
        /// </summary>
        /// <param name="fuzzyVariableName">Алиас нечеткой переменной</param>
        public FuzzyVariable getFuzzyVariableByName(string fuzzyVariableName)
        {
            fuzzyVariableName = fuzzyVariableName.Trim(' ', '\t', '"');
            List<FuzzyVariable> variables = dbConnection.Query<FuzzyVariable>("select * from FuzzyVariable where name = ? and deleted = '0' limit 1", fuzzyVariableName);
            if (variables.Count == 1)
            {
                return variables.ElementAt(0);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод подбора нечетких переменных по значениям.
        /// Используется при инициализации переменных, перед вычислением выражений ТО или ИНАЧЕ, и после обработки каждого из нечетких правил.
        /// Возвращает сущность <see cref="FuzzyVariableSelectionResult"/>.
        /// </summary>
        /// <param name="variables">Хранилище переменных</param>
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

        /// <summary>
        /// Метод подбора нечетких переменных по списку лексем.
        /// Используется перед вычислением выражений ЕСЛИ.
        /// Возвращает сущность <see cref="FuzzyVariableSelectionResult"/>.
        /// </summary>
        /// <param name="lexemes">Список лексем</param>
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
        
        /// <summary>
        /// Метод вычисления диапазона четких значений нечеткой переменных.
        /// Производит изменения в сущности нечеткой переменной и сохраняет их в базу данных.
        /// </summary>
        /// <param name="fuzzyVariableId">ID нечеткой переменной</param>
        public void calculateFuzzyRanges(int fuzzyVariableId)
        {
            //Searching for linguistic variable
            int linguisticVariableId = dbConnection.Query<FuzzyVariable>(
                    "select * from FuzzyVariable where id = ? and deleted = '0' limit 1",
                    fuzzyVariableId
                ).ElementAt(0).linguisticVariableId;

            //First and last fuzzy variable points
            List<Tuple<double, double>> points = new List<Tuple<double, double>>();
            double? minValue = null, maxValue = null;

            //Selecting fuzzy variables
            List<FuzzyVariable> fuzzyVariables =
                dbConnection.Query<FuzzyVariable>(
                    "select fv.* from FuzzyVariable fv " +
                    "join FuzzyVariableValue fvv on fvv.fuzzyVariableId = fv.id " +
                    "where fv.linguisticVariableId = ? and fv.deleted = '0' and fvv.deleted = '0' " +
                    "group by fv.id " +
                    "order by min(fvv.value)", 
                    linguisticVariableId
                );

            //Work with every fuzzy variable
            for (int i = 0; i < fuzzyVariables.Count; i++)
            {
                //Select fuzzy variable values
                List<FuzzyVariableValue> fuzzyVariableValues = 
                    dbConnection.Query<FuzzyVariableValue>(
                        "select * from FuzzyVariableValue where fuzzyVariableId = ? and deleted = '0' order by value", 
                        fuzzyVariables[i].id
                    );

                int count = fuzzyVariableValues.Count;
                //If fuzzy variable has values
                if (count > 0)
                {
                    //Setting min and max value for linguistic variable range
                    if (i == 0)
                    {
                        minValue = fuzzyVariableValues[0].value;
                    }
                    if (i == fuzzyVariables.Count - 1)
                    {
                        maxValue = fuzzyVariableValues[count - 1].value;
                    }

                    //Adding first 2 points
                    if (i != 0 && count >= 2)
                    {
                        if (fuzzyVariableValues[0].possibility != 0)
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, 0));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, fuzzyVariableValues[0].possibility));
                        }
                        else
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[0].value, 0));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[1].value, fuzzyVariableValues[1].possibility));
                        }
                    }

                    //Adding last 2 points
                    if (i != fuzzyVariables.Count - 1 && count >= 2)
                    {
                        if (fuzzyVariableValues[count - 1].possibility != 0)
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, fuzzyVariableValues[count - 1].possibility));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, 0));
                        }
                        else
                        {
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 2].value, fuzzyVariableValues[count - 2].possibility));
                            points.Add(new Tuple<double, double>(fuzzyVariableValues[count - 1].value, 0));
                        }
                    }
                }
            }

            //We should not continue if min and max values are not initialized
            if (!minValue.HasValue || !maxValue.HasValue)
            {
                return;
            }

            //List of intersection points (only X - value coordinate)
            List<double> intersections = new List<double>();

            //Counting X coordinate of intersection point
            for (int indx = 0; indx < points.Count; indx += 4)
            {
                double x1, x2, x3, x4, y1, y2, y3, y4;
                try
                {
                    x1 = points[indx].Item1;
                    x2 = points[indx + 1].Item1;
                    x3 = points[indx + 2].Item1;
                    x4 = points[indx + 3].Item1;

                    y1 = points[indx].Item2;
                    y2 = points[indx + 1].Item2;
                    y3 = points[indx + 2].Item2;
                    y4 = points[indx + 3].Item2;
                }
                catch (Exception)
                {
                    //TODO: throw error message in log?
                    break;
                }
                intersections.Add(
                    ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4))
                    /
                    ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4))
                );
            }

            //Creating ranges
            List<Tuple<double, double>> ranges = new List<Tuple<double, double>>();
            for (int indx = 0; indx < intersections.Count; indx++)
            {
                if (indx == 0)
                {
                    ranges.Add(new Tuple<double, double>(minValue.Value, intersections[indx]));
                }
                else if (indx == intersections.Count - 1)
                {
                    ranges.Add(new Tuple<double, double>(intersections[indx - 1], intersections[indx]));
                    ranges.Add(new Tuple<double, double>(intersections[indx], maxValue.Value));
                }
                else
                {
                    ranges.Add(new Tuple<double, double>(intersections[indx - 1], intersections[indx]));
                }
            }

            //We should continue only if count of ranges and count of fuzzy variables are equal
            if (ranges.Count != fuzzyVariables.Count)
            {
                return;
            }

            //Updating ranges in fuzzy variable
            for (int i = 0; i< fuzzyVariables.Count; i++)
            {
                fuzzyVariables[i].rangeStart = ranges[i].Item1;
                fuzzyVariables[i].rangeEnd = ranges[i].Item2;
                dbConnection.Update(fuzzyVariables[i]);
            }
        }

        /// <summary>
        /// Операция сравнения четкого значения с нечетким значением.
        /// Возвращает true, если четкое значение входит в диапазон четких значений нечеткой переменной.
        /// Операция коммутативна.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyEquals(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (intValue >= fuzzyVariable.rangeStart) && (intValue <= fuzzyVariable.rangeEnd);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция неравенства между четким и нечетким значением.
        /// Возвращает true, если четкое значение не входит в диапазон четких значений нечеткой переменной.
        /// Операция коммутативна.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyNotEquals(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (intValue < fuzzyVariable.rangeStart) || (intValue > fuzzyVariable.rangeEnd);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше" между четким и нечетким значениями.
        /// Возвращает true, если четкое значение больше, чем конец диапазона четких значений нечеткой переменной.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyMore(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (intValue > fuzzyVariable.rangeEnd);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше" между нечетким и четким значениями.
        /// Возвращает true, если начало диапазона четких значений нечеткой переменных больше, чем четкое значение.
        /// </summary>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        /// <param name="intValue">Четкое значение</param>
        public bool fuzzyMore(string fuzzyValue, int intValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (fuzzyVariable.rangeStart > intValue);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше" между двумя нечеткими значениями.
        /// Возвращает true, если начало диапазона четких значений первой нечеткой переменной больше, чем начало диапазона четких значений второй нечеткой переменной.
        /// Данное сравнение корректно, так как все диапазоны четких значений нечетной переменной идут строго друг за другом и не пересекаются.
        /// </summary>
        /// <param name="fuzzyValue1">Первое нечеткое значение</param>
        /// <param name="fuzzyValue2">Второе нечеткое значение</param>
        public bool fuzzyMore(string fuzzyValue1, string fuzzyValue2)
        {
            FuzzyVariable fuzzyVariable1 = getFuzzyVariableByName(fuzzyValue1);
            FuzzyVariable fuzzyVariable2 = getFuzzyVariableByName(fuzzyValue2);
            if (fuzzyVariable1 != null && fuzzyVariable2 != null)
            {
                return fuzzyVariable1.rangeEnd > fuzzyVariable2.rangeEnd;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше либо равно" между четким и нечетким значениями.
        /// Возвращает true, если четкое значение больше, чем конец диапазона четких значений нечеткой переменной, или находится в пределах этого диапазона.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyMoreOrEquals(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return ((intValue >= fuzzyVariable.rangeStart) && (intValue <= fuzzyVariable.rangeEnd)) || (intValue > fuzzyVariable.rangeEnd);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше" между нечетким и четким значениями.
        /// Возвращает true, если начало диапазона четких значений нечеткой переменной больше либо равно нечеткому значению, или четкое значение находится в пределах этого диапазона.
        /// </summary>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        /// <param name="intValue">Четкое значение</param>
        public bool fuzzyMoreOrEquals(string fuzzyValue, int intValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return ((intValue >= fuzzyVariable.rangeStart) && (intValue <= fuzzyVariable.rangeEnd)) || (fuzzyVariable.rangeStart >= intValue);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "больше либо равно" между двумя нечеткими значениями.
        /// Возвращает true, если конец диапазона четких значений первой нечеткой переменной больше либо равен концу диапазона четких значений второй нечеткой переменной.
        /// </summary>
        /// <param name="fuzzyValue1">Первое нечеткое значение</param>
        /// <param name="fuzzyValue2">Второе нечеткое значение</param>
        public bool fuzzyMoreOrEquals(string fuzzyValue1, string fuzzyValue2)
        {
            FuzzyVariable fuzzyVariable1 = getFuzzyVariableByName(fuzzyValue1);
            FuzzyVariable fuzzyVariable2 = getFuzzyVariableByName(fuzzyValue2);
            if (fuzzyVariable1 != null && fuzzyVariable2 != null)
            {
                return fuzzyVariable1.rangeEnd >= fuzzyVariable2.rangeEnd;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Операция "меньше" между четким и нечетким значениями.
        /// Возвращает true, если четкое значение меньше, чем начало диапазона четких значений нечеткой переменной.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyLess(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (intValue < fuzzyVariable.rangeStart);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "меньше" между нечетким и четким значениями.
        /// Возвращает true, если конец диапазона четких значений нечеткой переменной меньше, чем четкое значение.
        /// </summary>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        /// <param name="intValue">Четкое значение</param>
        public bool fuzzyLess(string fuzzyValue, int intValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (fuzzyVariable.rangeEnd < intValue);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "меньше" между двумя нечеткими значениями.
        /// Возвращает true, если конец диапазона четких значений первой нечеткой переменной меньше, чем конец диапазона четких значений второй нечеткой переменной.
        /// </summary>
        /// <param name="fuzzyValue1">Первое нечеткое значение</param>
        /// <param name="fuzzyValue2">Второе нечеткое значение</param>
        public bool fuzzyLess(string fuzzyValue1, string fuzzyValue2)
        {
            FuzzyVariable fuzzyVariable1 = getFuzzyVariableByName(fuzzyValue1);
            FuzzyVariable fuzzyVariable2 = getFuzzyVariableByName(fuzzyValue2);
            if (fuzzyVariable1 != null && fuzzyVariable2 != null)
            {
                return fuzzyVariable1.rangeEnd < fuzzyVariable2.rangeEnd;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "меньше либо равно" между четким и нечетким значениями.
        /// Возвращает true, если четкое значение меньше, чем начало диапазона четких значений нечеткой переменной, или находится в пределах этого диапазона.
        /// </summary>
        /// <param name="intValue">Четкое значение</param>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        public bool fuzzyLessOrEquals(int intValue, string fuzzyValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (intValue < fuzzyVariable.rangeStart) || ((intValue >= fuzzyVariable.rangeStart) && (intValue <= fuzzyVariable.rangeEnd));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "меньше либо равно" между нечетким и четким значениями.
        /// Возвращает true, если конец диапазона четких значение нечеткой переменной меньше значения четкой переменной, или значение четкой переменной находится в пределах этого диапазона.
        /// </summary>
        /// <param name="fuzzyValue">Нечеткое значение</param>
        /// <param name="intValue">Четкое значение</param>
        public bool fuzzyLessOrEquals(string fuzzyValue, int intValue)
        {
            FuzzyVariable fuzzyVariable = getFuzzyVariableByName(fuzzyValue);
            if (fuzzyVariable != null)
            {
                return (fuzzyVariable.rangeEnd < intValue) || ((intValue >= fuzzyVariable.rangeStart) && (intValue <= fuzzyVariable.rangeEnd));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Операция "меньше либо равно" между двумя нечеткими значениями.
        /// Возвращает true, если конец диапазона четких значений первой нечеткой переменной меньше, либо равен концу диапазона четких значений второй нечеткой переменной.
        /// </summary>
        /// <param name="fuzzyValue1">Первое нечеткое значение</param>
        /// <param name="fuzzyValue2">Второе нечеткое значение</param>
        public bool fuzzyLessOrEquals(string fuzzyValue1, string fuzzyValue2)
        {
            FuzzyVariable fuzzyVariable1 = getFuzzyVariableByName(fuzzyValue1);
            FuzzyVariable fuzzyVariable2 = getFuzzyVariableByName(fuzzyValue2);
            if (fuzzyVariable1 != null && fuzzyVariable2 != null)
            {
                return fuzzyVariable1.rangeEnd <= fuzzyVariable2.rangeEnd;
            }
            else
            {
                return false;
            }
        }
    }
}
