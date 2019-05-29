namespace master_piece.service.init_variables
{
    /// <summary>
    /// Представление целочисленной переменной в алгоритме
    /// </summary>
    class IntViewVariable : AbstractViewVariable
    {
        /// <summary>
        /// Значение переменной
        /// </summary>
        public new int value { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="name">Алиас</param>
        /// <param name="value">Значение</param>
        public IntViewVariable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="name">Алиас</param>
        /// <param name="value">Значение</param>
        /// <param name="firstReassignmentLevel">Уровень переопределения</param>
        public IntViewVariable(string name, int value, int firstReassignmentLevel)
        {
            this.name = name;
            this.value = value;
            this.firstReassignmentLevel = firstReassignmentLevel;
        }
    }
}
