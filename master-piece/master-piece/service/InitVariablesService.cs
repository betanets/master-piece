using master_piece.variable;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.service
{
    class InitVariablesService
    {
        public static List<IntVariable> initIntVariables(DataGridViewRowCollection dgvrCollection, int newRowIndex, RichTextBox logComponent)
        {
            logComponent.AppendText("\n\n----------Ввод переменных:----------------\n");

            List<IntVariable> intVariables = new List<IntVariable>();

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

                int intVariableValue;
                try
                {
                    intVariableValue = Convert.ToInt32(dgvr.Cells[1].Value.ToString());
                }
                catch (Exception)
                {
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": значение переменной не задано или введено неверно\n");
                    processingTerminated = true;
                    break;
                }

                IntVariable intVariable = new IntVariable(dgvr.Cells[0].Value.ToString(), intVariableValue);
                intVariables.Add(intVariable);
            }

            if (!processingTerminated)
            {
                LoggerService.logIntVariables(logComponent, intVariables);
            }

            return intVariables;
        }
    }
}
