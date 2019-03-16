using SQLite;
using SQLiteNetExtensions.Attributes;

namespace master_piece.domain
{
    /**
     * Нечеткая переменная в базе данных
     */ 
    public class FuzzyVariable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [ForeignKey(typeof(LinguisticVariable)), NotNull]
        public int linguisticVariableId { get; set; }

        [NotNull]
        public string name { get; set; }

        [NotNull]
        public int deleted { get; set; } = 0;
    }
}
