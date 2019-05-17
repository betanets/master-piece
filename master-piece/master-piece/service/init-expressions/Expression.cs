namespace master_piece.service.init_expressions
{
    /// <summary>
    /// Сущность выражения
    /// </summary>
    class Expression
    {
        /// <summary>
        /// Порядковый номер выражения в таблице
        /// </summary>
        public int expressionLevel;
        
        /// <summary>
        /// Выражение ЕСЛИ в текстовом виде
        /// </summary>
        public string ifExpressionText;

        /// <summary>
        /// Выражение ТО в текстовом виде
        /// </summary>
        public string thenExpressionText;

        /// <summary>
        /// Выражение ИНАЧЕ в текстовом виде
        /// </summary>
        public string elseExpressionText;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="expressionLevel">Номер выражения</param>
        /// <param name="ifExpressionText">Выражение ЕСЛИ в текстовом виде</param>
        /// <param name="thenExpressionText">Выражение ТО в текстовом виде</param>
        /// <param name="elseExpressionText">Выражение ИНАЧЕ в текстовом виде</param>
        public Expression(int expressionLevel, string ifExpressionText, string thenExpressionText, string elseExpressionText)
        {
            this.expressionLevel = expressionLevel;
            this.ifExpressionText = ifExpressionText;
            this.thenExpressionText = thenExpressionText;
            this.elseExpressionText = elseExpressionText;
        }
    }
}
