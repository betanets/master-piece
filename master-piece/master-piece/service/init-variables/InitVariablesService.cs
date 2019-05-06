using master_piece.service.init_variables;
using master_piece.variable;
using System;
using System.Windows.Forms;

namespace master_piece.service
{
    class InitVariablesService
    {
        public static VariablesStorage initIntVariables(DataGridViewRowCollection dgvrCollection, int newRowIndex, RichTextBox logComponent)
        {
            logComponent.AppendText("\n\n----------Ввод переменных:----------------\n");

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
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": не все ячейки заполнены\n");
                    break;
                }

                if(dgvr.Cells[0].Value == null || dgvr.Cells[0].Value.ToString().Length == 0)
                {
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": не задан идентификатор переменной\n");
                    processingTerminated = true;
                    break;
                }

                try
                {
                    string value = dgvr.Cells[1].Value.ToString();
                    if (value.StartsWith("\""))
                    {
                        //TODO: set id instead of 0!
                        FuzzyViewVariable fuzzyVariable = new FuzzyViewVariable(dgvr.Cells[0].Value.ToString(), value, 0);
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
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": значение переменной не задано или введено неверно\n");
                    processingTerminated = true;
                    break;
                }
            }

            if (!processingTerminated)
            {
                LoggerService.logVariables(logComponent, variablesStorage);
            }

            return variablesStorage;
        }
    }
}
