using master_piece.variable;
using System.Collections.Generic;

namespace master_piece.service.init_variables
{
    class VariablesStorage
    {
        public List<IntViewVariable> intVariables = new List<IntViewVariable>();
        public List<FuzzyViewVariable> fuzzyVariables = new List<FuzzyViewVariable>();

        public void Clear()
        {
            intVariables.Clear();
            fuzzyVariables.Clear();
        }
    }
}
