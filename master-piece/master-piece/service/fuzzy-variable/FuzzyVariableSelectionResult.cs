namespace master_piece.service.fuzzy_variable
{
    /// <summary>
    /// Сущность результата подбора нечеткой переменной по значению
    /// </summary>
    class FuzzyVariableSelectionResult
    {
        /// <summary>
        /// Флаг, указывающий, завершился ли подбор успешно.
        /// По умолчанию: false.
        /// </summary>
        public bool isSuccess = false;

        /// <summary>
        /// Сообщение с результатом подбора нечеткой переменной.
        /// Сообщение - null, если подбор завершился успешно.
        /// </summary>
        public string messageString;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="isSuccess">Флаг, указывающий, завершился ли подбор успешно</param>
        /// <param name="messageString">Сообщение с результатом подбора нечеткой переменной</param>
        public FuzzyVariableSelectionResult(bool isSuccess, string messageString)
        {
            this.isSuccess = isSuccess;
            this.messageString = messageString;
        }
    }
}
