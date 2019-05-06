namespace master_piece.variable
{
    class IntViewVariable : AbstractViewVariable
    {
        public new int value { get; set; }

        public IntViewVariable(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
