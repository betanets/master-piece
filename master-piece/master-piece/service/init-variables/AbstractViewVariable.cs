namespace master_piece.variable
{
    abstract class AbstractViewVariable
    {
        public string name { get; set; }
        protected object value { get; set; }
        public int firstReassignmentLevel { get; set; } = -1;
    }
}
