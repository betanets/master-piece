namespace master_piece.service.fuzzy_variable
{
    class FuzzyVariableSelectionResult
    {
        public bool isSuccess = false;
        public string messageString;

        public FuzzyVariableSelectionResult(bool isSuccess, string messageString)
        {
            this.isSuccess = isSuccess;
            this.messageString = messageString;
        }
    }
}
