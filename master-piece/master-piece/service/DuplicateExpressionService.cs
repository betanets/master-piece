using master_piece.variable;
using System.Collections.Generic;

namespace master_piece.service
{
    class DuplicateExpressionService
    {
        public static void markDuplicates(List<Subexpression> expressions, List<IntVariable> intVariablesStorage)
        {
            List<Subexpression> deduplicatedExpressions = new List<Subexpression>();

            foreach(Subexpression exp in expressions)
            {
                bool duplicateFound = false;
                foreach(Subexpression dde in deduplicatedExpressions)
                {
                    if(exp.Equals(dde))
                    {
                        dde.mustBePrecalculated = true;
                        exp.mustBePrecalculated = true;
                        duplicateFound = true;
                        //No need to break: we should add all same expressions with different levels
                    }
                }
                if(!duplicateFound)
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
                    List<IntVariable> intVariables = SemanticService.getIntVariablesBySubexpression(exp, intVariablesStorage);
                    bool mustBeFiltered = false;
                    foreach (IntVariable iv in intVariables)
                    {
                        if (iv.firstReassignmentLevel != -1 && iv.firstReassignmentLevel < exp.expressionLevel)
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
