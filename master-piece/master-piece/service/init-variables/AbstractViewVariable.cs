﻿namespace master_piece.service.init_variables
{
    /// <summary>
    /// Абстрактное представление переменной в алгоритме
    /// </summary>
    abstract class AbstractViewVariable
    {
        /// <summary>
        /// Алиас переменной
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Значение переменной
        /// </summary>
        protected object value { get; set; }
        
        /// <summary>
        /// Уровень, на котором значение переменной было переопределено в первый раз.
        /// По умолчанию уровень: -1
        /// </summary>
        public int firstReassignmentLevel { get; set; } = -1;
    }
}
