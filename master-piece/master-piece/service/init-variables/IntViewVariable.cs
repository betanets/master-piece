namespace master_piece.variable
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
    }
}
