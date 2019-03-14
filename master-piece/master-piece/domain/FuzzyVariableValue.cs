using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master_piece.domain
{
    class FuzzyVariableValue
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed]
        public int fuzzyVariableId { get; set; }
        public double value { get; set; }
        public double possibility { get; set; }
    }
}
