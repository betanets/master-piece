namespace master_piece.variable
{
    abstract class AbstractVariable
    {
        public string name { get; set; }
        protected object value { get; set; }
        public int firstReassignmentLevel { get; set; } = -1;
    }
}
