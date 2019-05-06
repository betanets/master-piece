namespace master_piece.variable
{
    class FuzzyViewVariable : AbstractViewVariable
    {
        public new string value { get; set; }

        /// <summary>
        /// ID of fuzzy variable in database
        /// </summary>
        public int fuzzyVariableId { get; }

        public FuzzyViewVariable(string name, string value, int fuzzyVariableId)
        {
            this.name = name;
            this.value = value;
        }
    }
}
