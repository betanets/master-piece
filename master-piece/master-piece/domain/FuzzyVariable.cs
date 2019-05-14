using SQLite;
using SQLiteNetExtensions.Attributes;

namespace master_piece.domain
{
    /**
     * Нечеткая переменная в базе данных
     */ 
    public class FuzzyVariable
    {
        /// <summary>
        /// ID сущности
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// ID лингвистической переменной
        /// </summary>
        [ForeignKey(typeof(LinguisticVariable)), NotNull]
        public int linguisticVariableId { get; set; }

        /// <summary>
        /// Алиас нечеткой переменной
        /// </summary>
        [NotNull]
        public string name { get; set; }

        /// <summary>
        /// Начало диапазона четких значений нечеткой переменной.
        /// По умолчанию: 0
        /// </summary>
        [NotNull]
        public double rangeStart { get; set; } = 0;

        /// <summary>
        /// Конец диапазона четких значений нечеткой переменной.
        /// По умолчанию: 0
        /// </summary>
        [NotNull]
        public double rangeEnd { get; set; } = 0;

        /// <summary>
        /// Флаг, указывающий, была ли удалена запись.
        /// По умолчанию: 0 (запись не удалена)
        /// </summary>
        [NotNull]
        public int deleted { get; set; } = 0;
    }
}
