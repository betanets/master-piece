using System.Collections.Generic;

namespace master_piece.service
{
    class DuplicateExpressionService
    {
        public static List<Subexpression> findDuplicates(List<Subexpression> expressions)
        {
            List<Subexpression> duplicates = new List<Subexpression>();
            List<Subexpression> deduplicatedExpressions = new List<Subexpression>();

            foreach(Subexpression exp in expressions)
            {
                bool duplicateFound = false;
                foreach(Subexpression dde in deduplicatedExpressions)
                {
                    if(exp.Equals(dde))
                    {
                        duplicates.Add(exp);
                        duplicateFound = true;
                        break;
                    }
                }
                if(!duplicateFound)
                {
                    deduplicatedExpressions.Add(exp);
                }
            }

            return duplicates;
        }
    }
}
