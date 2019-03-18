using master_piece.variable;
using static master_piece.service.Operation;

namespace master_piece.service
{
    class CommonSubexpression
    {
        private AbstractVariable variableFirst;
        private OperationEnum? operation;
        private AbstractVariable variableSecond;

        //Expression is leaf: it contains only one variable (possibly assigned to constant value)
        private bool isLeaf()
        {
            return operation == null && variableSecond == null;
        }
    }
}
