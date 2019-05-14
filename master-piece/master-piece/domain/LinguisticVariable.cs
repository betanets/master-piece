using SQLite;

namespace master_piece.domain
{
    /**
     * Лингвистическая переменная в базе данных
     */
    public class LinguisticVariable
    {
        /// <summary>
        /// ID сущности
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// Алиас лингвистической переменной
        /// </summary>
        [NotNull]
        public string name { get; set; }

        /// <summary>
        /// Флаг, указывающий, была ли удалена запись.
        /// По умолчанию: 0 (запись не удалена)
        /// </summary>
        [NotNull]
        public int deleted { get; set; } = 0;
    }
}
