namespace master_piece.subexpression
{
    class Expression
    {
        /// <summary>
        /// Expression's serial number in list
        /// </summary>
        public int expressionLevel;
        
        /// <summary>
        /// IF expression in plain text
        /// </summary>
        public string ifExpressionText;

        /// <summary>
        /// THEN expression in plain text
        /// </summary>
        public string thenExpressionText;

        /// <summary>
        /// ELSE expression in plain text
        /// </summary>
        public string elseExpressionText;

        /// <summary>
        /// Default constructor for expression
        /// </summary>
        public Expression(int expressionLevel, string ifExpressionText, string thenExpressionText, string elseExpressionText)
        {
            this.expressionLevel = expressionLevel;
            this.ifExpressionText = ifExpressionText;
            this.thenExpressionText = thenExpressionText;
            this.elseExpressionText = elseExpressionText;
        }
    }
}
