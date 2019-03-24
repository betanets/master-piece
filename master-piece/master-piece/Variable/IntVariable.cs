namespace master_piece.variable
{
    class IntVariable : AbstractVariable
    {
        public new int value { get; set; }

        public IntVariable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
