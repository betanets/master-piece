using SQLite;

namespace master_piece.domain
{
    /**
     * Одно из значений нечеткой переменной в базе данных (пара значение - вероятность)
     */
    public class FuzzyVariableValue
    {
        /// <summary>
        /// ID сущности
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// ID нечеткой переменной
        /// </summary>
        [Indexed, NotNull]
        public int fuzzyVariableId { get; set; }

        /// <summary>
        /// Значение элемента нечеткого множества
        /// </summary>
        [NotNull]
        public double value { get; set; }

        /// <summary>
        /// Вероятность истинности для элемента нечеткого множества
        /// </summary>
        [NotNull]
        public double possibility { get; set; }

        /// <summary>
        /// Флаг, указывающий, была ли удалена запись.
        /// По умолчанию: 0 (запись не удалена)
        /// </summary>
        [NotNull]
        public int deleted { get; set; } = 0;
    }
}
