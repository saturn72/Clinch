namespace ClinchSpreadsheet
{
    public sealed class Cell
    {
        public string? Value { get; init; }
        internal IEnumerable<AritmeticOperation>? FormulaOperations { get; set; }
    }
}
