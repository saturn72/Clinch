namespace ClinchSpreadsheet
{
    public sealed class AritmeticOperation
    {
        public AritmeticOperation(string constOrCellId, char? operand)
        {
            ConstOrCellId = constOrCellId;
            Operand = operand;
        }
        public string ConstOrCellId { get; }
        public char? Operand { get; }
    }
}
