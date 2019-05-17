using master_piece.service.init_variables;
using master_piece.service.lexical_analysis;
using master_piece.service.subexpression;
using System.Collections.Generic;

namespace master_piece.service.duplicate
{
    /// <summary>
    /// Сервис выделения дубликатов подвыражений
    /// </summary>
    class DuplicateExpressionService
    {
        /// <summary>
        /// Метод выделения дубликатов подвыражений.
        /// Если выражение - дубликат, флаг mustBePrecalculated будет установлен в значение true.
        /// Флаг устанавливается для ВСЕХ подвыражений, включая первое.
        /// </summary>
        /// <param name="expressions">Список подвыражений</param>
        /// <param name="variablesStorage">Хранилище переменных</param>
        public static void markDuplicates(List<Subexpression> expressions, VariablesStorage variablesStorage)
        {
            List<Subexpression> deduplicatedExpressions = new List<Subexpression>();

            foreach (Subexpression exp in expressions)
            {
                bool duplicateFound = false;
                foreach (Subexpression dde in deduplicatedExpressions)
                {
                    if (exp.Equals(dde))
                    {
                        dde.mustBePrecalculated = true;
                        exp.mustBePrecalculated = true;
                        duplicateFound = true;
                        //No need to break: we should add all same expressions with different levels
                    }
                }
                if (!duplicateFound)
                {
                    deduplicatedExpressions.Add(exp);
                }
            }

            //Checking semantic and filtering duplicates: if variable already reassigned, we should NOT add subexpression into duplicates
            //Backward traverse used only for correct remove
            foreach (Subexpression exp in expressions)
            {
                if (exp.mustBePrecalculated == true)
                {
                    List<AbstractViewVariable> abstractVariables = LexicalAnalysisService.getVariablesBySubexpression(exp, variablesStorage);
                    bool mustBeFiltered = false;
                    foreach (AbstractViewVariable av in abstractVariables)
                    {
                        if (av.firstReassignmentLevel != -1 && av.firstReassignmentLevel < exp.expressionLevel)
                        {
                            mustBeFiltered = true;
                            break;
                        }
                    }

                    if (mustBeFiltered)
                    {
                        exp.mustBePrecalculated = false;
                    }
                }
            }
        }
    }
}
