using SQLite;

namespace master_piece.domain
{
    /**
     * Лингвистическая переменная в базе данных
     */
    public class LinguisticVariable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public string name { get; set; }

        [NotNull]
        public int deleted { get; set; } = 0;
    }
}
