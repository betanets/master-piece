namespace master_piece.service.init_variables
{
    /// <summary>
    /// Представление нечеткой переменной в алгоритме
    /// </summary>
    class FuzzyViewVariable : AbstractViewVariable
    {
        /// <summary>
        /// Нечеткое значение переменной
        /// </summary>
        public new string value { get; set; }

        /// <summary>
        /// ID нечеткой переменной в базе данных.
        /// Может быть null
        /// </summary>
        public int? fuzzyVariableId { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="name">Алиас</param>
        /// <param name="value">Нечеткое значение</param>
        public FuzzyViewVariable(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="name">Алиас</param>
        /// <param name="value">Нечеткое значение</param>
        /// /// <param name="firstReassignmentLevel">Уровень переопределения</param>
        public FuzzyViewVariable(string name, string value, int firstReassignmentLevel)
        {
            this.name = name;
            this.value = value;
            this.firstReassignmentLevel = firstReassignmentLevel;
        }
    }
}
