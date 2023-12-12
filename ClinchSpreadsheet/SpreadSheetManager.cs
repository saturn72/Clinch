
using System.Text.RegularExpressions;

namespace ClinchSpreadsheet
{
    //the primary interface to the spread sheet

    public class SpreadSheetManager
    {
        private readonly IDictionary<string, Cell> _cells;
        private static readonly char[] Operands = new[] { '-', '+', '*', '/' };
        public SpreadSheetManager() : this(new Dictionary<string, Cell>(StringComparer.InvariantCultureIgnoreCase))
        {

        }
        public SpreadSheetManager(IDictionary<string, Cell> cells)
        {
            _cells = cells;
        }


        public void Set(string cellId, string value)
        {
            //this creates new instance for each set. can use cell value override

            var cell = new Cell { Value = value };
            if (value.StartsWith("="))
                cell.FormulaOperations = GetAritmeticOperations(value);

            _cells[cellId] = cell;
        }

        private IEnumerable<AritmeticOperation> GetAritmeticOperations(string value)
        {
            var fo = new List<AritmeticOperation>();

            var f = value[1..];
            int idx;

            do
            {
                idx = f.IndexOfAny(Operands);
                if (idx != -1)
                {
                    fo.Add(new(f[0..idx], f[idx]));
                    f = f.Substring(idx + 1);
                }
                else
                {
                    fo.Add(new(f, null));
                }
            }
            while (idx >= 0);
            return fo;
        }

        public string Get(string cellId)
        {
            //not exists
            if (!_cells.TryGetValue(cellId, out var value) ||
                value == null ||
                value.Value == default)
                return "0";

            //not a formula
            if (value.FormulaOperations == null)
                return value.Value;

            //calculates formula
            return CaluculateFormula(value);
        }

        private string CaluculateFormula(Cell? cell)
        {
            float valueSoFar = 0;

            for (var i = 0; i < cell.FormulaOperations.Count(); i++)
            {
                var cur = cell.FormulaOperations.ElementAt(i);
                //is cellId
                var currentValue = Regex.IsMatch(cur.ConstOrCellId, @"^\d+") ?
                    cur.ConstOrCellId :
                    Get(cur.ConstOrCellId);

                //first iteration
                if (i == 0)
                {
                    valueSoFar = float.Parse(currentValue);
                    continue;
                }

                //fetch the operand form previous element
                var prev = cell.FormulaOperations.ElementAt(i - 1);

                if (prev.Operand == '/' && currentValue == "0")
                    return "invalid-operation-division-by-zero";

                valueSoFar = Calculate(prev.Operand.Value, float.Parse(currentValue), valueSoFar);
            }
            return valueSoFar.ToString();
        }
        public static float Calculate(char op, float current, float valueSoFar) => op switch
        {
            '+' => valueSoFar + current,
            '-' => valueSoFar - current,
            '/' => valueSoFar / current,
            '*' => valueSoFar * current,
            _ => throw new ArgumentOutOfRangeException(nameof(op), $"Not expected direction value. use only one of: \'{Operands}\'"),
        };
    }
}