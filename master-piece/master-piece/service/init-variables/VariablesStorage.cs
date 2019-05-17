using System.Collections.Generic;

namespace master_piece.service.init_variables
{
    /// <summary>
    /// Хранилище переменных
    /// </summary>
    class VariablesStorage
    {
        /// <summary>
        /// Список целочисленных переменных.
        /// По умолчанию - инициализирован пустым списком
        /// </summary>
        public List<IntViewVariable> intVariables = new List<IntViewVariable>();

        /// <summary>
        /// Список нечетких переменных.
        /// По умолчанию - инициализирован пустым списком
        /// </summary>
        public List<FuzzyViewVariable> fuzzyVariables = new List<FuzzyViewVariable>();

        /// <summary>
        /// Метод очистки хранилища.
        /// Очищаются списки целочисленных переменных и нечетких переменных
        /// </summary>
        public void Clear()
        {
            intVariables.Clear();
            fuzzyVariables.Clear();
        }
    }
}
