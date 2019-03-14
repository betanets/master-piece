using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master_piece.domain
{
    class FuzzyVariable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string shortName { get; set; }
        public string fullName { get; set; }
    }
}
