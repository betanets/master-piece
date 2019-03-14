using System;
using System.Collections.Generic;

namespace master_piece.Variable
{
    class FuzzyVariable
    {
        private string shortName { get; set; }
        private string fullName { get; set; }
        private List<Tuple<double, double>> values { get; set; }
    }
}
