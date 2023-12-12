using Shouldly;
using Xunit;

namespace ClinchSpreadsheet.Tests
{
    public class SpreadsheetManagerTests
    {
        [Fact]
        public void Set_ValueToCell()
        {
            var cells = new Dictionary<string, Cell>();
            var sm = new SpreadSheetManager(cells);
            var cellId = "a1";
            var value = "3.123";

            sm.Set(cellId, value);
            cells.ShouldContain(x => x.Key == cellId && x.Value.Value == value);
        }

        [Fact]
        public void Set_FormulaToCell_const()
        {
            var cells = new Dictionary<string, Cell>();
            var sm = new SpreadSheetManager(cells);
            var cellId = "a1";

            sm.Set(cellId, "=A1+A2");
            cells[cellId].Value.ShouldBe("=A1+A2");
        }

        [Fact]
        public void Set_FormulaToCell_Cell_formula_constant()
        {
            var cells = new Dictionary<string, Cell>();
            var sm = new SpreadSheetManager(cells);
            var cellId = "a1";

            sm.Set(cellId, "=5.369");
            cells[cellId].Value.ShouldBe("=5.369");
        }
        [Fact]
        public void Get_CellValue()
        {
            var cellId = "a1";
            var value = "3.123";

            var cells = new Dictionary<string, Cell>
            {
                {cellId, new Cell{ Value = value }}
            };
            var sm = new SpreadSheetManager(cells);
            sm.Get(cellId).ShouldBe(value);

            //cell not exists - return 0
            sm.Get("a2").ShouldBe("0");
        }

        [Fact]
        public void Get_CellFormula()
        {
            var cellId = "a1";
            var formula = "3.123";

            var cells = new Dictionary<string, Cell>
            {
                {cellId, new Cell{ Value = formula }}
            };
            var sm = new SpreadSheetManager(cells);
            sm.Get(cellId).ShouldBe(formula);

            //cell not exists - return 0
            sm.Get("a2").ShouldBe("0");
        }
    }
}