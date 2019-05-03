using master_piece.subexpression;
using System.Collections.Generic;
using System.Windows.Forms;

namespace master_piece.service
{
    class InitExpressionsService
    {
        public static List<Expression> initExpressions(DataGridViewRowCollection dgvrCollection, int newRowIndex, RichTextBox logComponent)
        {
            logComponent.AppendText("\n\n----------Ввод выражений:----------------\n");

            List<Expression> expressions = new List<Expression>();

            bool processingTerminated = false;

            int index = 1;
            foreach (DataGridViewRow dgvr in dgvrCollection)
            {
                if (dgvr.Index == newRowIndex)
                {
                    break;
                }

                if (dgvr.Cells[0].Value == null || dgvr.Cells[0].Value.ToString().Length == 0)
                {
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": не задано выражение ЕСЛИ\n");
                    processingTerminated = true;
                    break;
                }

                if (dgvr.Cells[1].Value == null || dgvr.Cells[1].Value.ToString().Length == 0)
                {
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + ": не задано выражение ТО\n");
                    processingTerminated = true;
                    break;
                }
                
                int openBracketCount = 0, closeBracketCount = 0;
                foreach (char c in dgvr.Cells[0].Value.ToString())
                {
                    if (c == '(')
                        openBracketCount++;
                    else if (c == ')')
                        closeBracketCount++;
                }

                if(openBracketCount != closeBracketCount)
                {
                    logComponent.AppendText("Ошибка в строке " + dgvr.Index.ToString() + 
                        ": количество открывающих скобок (" + openBracketCount + 
                        ") не совпадает с количеством закрывающих (" + closeBracketCount + ")\n");
                    processingTerminated = true;
                    break;
                }

                Expression expression = new Expression(index, dgvr.Cells[0].Value.ToString(), dgvr.Cells[1].Value.ToString(),
                    dgvr.Cells[2].Value != null ? dgvr.Cells[2].Value.ToString() : string.Empty);
                expressions.Add(expression);

                index++;
            }

            if (!processingTerminated)
            {
                LoggerService.logExpressions(logComponent, expressions);
            }

            return expressions;
        }
    }
}
