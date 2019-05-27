using master_piece.service.logging;
using System;
using System.Windows.Forms;

namespace master_piece.service.init_variables
{
    /// <summary>
    /// Сервис инициализации переменных
    /// </summary>
    class InitVariablesService
    {
        /// <summary>
        /// Метод инициализации переменных.
        /// Возвращает хранилище переменных <see cref="VariablesStorage"/>
        /// </summary>
        /// <param name="dgvrCollection">Строки с таблицы со списком выражений на форме MainForm</param>
        /// <param name="newRowIndex">Индекс новой строки в таблице</param>
        /// <param name="loggingService">Сервис логирования</param>
        public static VariablesStorage initVariables(DataGridViewRowCollection dgvrCollection, int newRowIndex, LoggingService loggingService)
        {
            loggingService.logString("\n\n----------Ввод переменных:----------------\n");

            VariablesStorage variablesStorage = new VariablesStorage();

            bool processingTerminated = false;

            foreach (DataGridViewRow dgvr in dgvrCollection)
            {
                if (dgvr.Index == newRowIndex)
                {
                    break;
                }

                if (dgvr.Cells.Count != 2)
                {
                    loggingService.logError("Ошибка в строке " + dgvr.Index.ToString() + ": не все ячейки заполнены\n");
                    break;
                }

                if(dgvr.Cells[0].Value == null || dgvr.Cells[0].Value.ToString().Length == 0)
                {
                    loggingService.logError("Ошибка в строке " + dgvr.Index.ToString() + ": не задан идентификатор переменной\n");
                    processingTerminated = true;
                    break;
                }

                try
                {
                    string value = dgvr.Cells[1].Value.ToString();
                    if (value.StartsWith("\""))
                    {
                        FuzzyViewVariable fuzzyVariable = new FuzzyViewVariable(dgvr.Cells[0].Value.ToString(), value);
                        variablesStorage.fuzzyVariables.Add(fuzzyVariable);
                    }
                    else
                    {
                        int intVariableValue = Convert.ToInt32(value);
                        IntViewVariable intVariable = new IntViewVariable(dgvr.Cells[0].Value.ToString(), intVariableValue);
                        variablesStorage.intVariables.Add(intVariable);
                    }
                }
                catch (Exception)
                {
                    loggingService.logError("Ошибка в строке " + dgvr.Index.ToString() + ": значение переменной не задано или введено неверно\n");
                    processingTerminated = true;
                    break;
                }
            }

            if (!processingTerminated)
            {
                loggingService.logVariables(variablesStorage);
            }

            return variablesStorage;
        }

        /// <summary>
        /// Метод получения значения представления переменной по алиасу
        /// </summary>
        /// <param name="identifier">Алиас переменной</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static object getValueFromVariablesStorage(string identifier, VariablesStorage variablesStorage)
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
