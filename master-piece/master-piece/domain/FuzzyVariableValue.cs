using SQLite;

namespace master_piece.domain
{
    /**
     * Значение нечеткой переменной в базе данных (пара значение - вероятность)
     */
    public class FuzzyVariableValue
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [Indexed, NotNull]
        public int fuzzyVariableId { get; set; }

        [NotNull]
        public double value { get; set; }

        [NotNull]
        public double possibility { get; set; }
    }
}
