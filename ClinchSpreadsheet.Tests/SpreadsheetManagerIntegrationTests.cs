using Shouldly;
using Xunit;

namespace ClinchSpreadsheet.Tests
{
    public class SpreadsheetManagerIntegrationTests
    {
        [Fact]
        public void Set_Get_Constants()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a3", "35");
            //cell not exists - return 0
            sm.Get("a3").ShouldBe("35");
        }


        [Fact]
        public void Set_Get_FormulaWithOnlyConstants()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a3", "=35");
            //cell not exists - return 0
            sm.Get("a3").ShouldBe("35");
        }


        [Fact]
        public void Set_Get_FormulaWithConstants()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a1", "20");
            sm.Set("a3", "=35+a1");
            //cell not exists - return 0
            sm.Get("a3").ShouldBe("55");
        }


        [Fact]
        public void Set_Get_FormulaWithOnlyCellValues()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a1", "20");
            sm.Set("a2", "15");
            sm.Set("a3", "=a1/a2");
            //cell not exists - return 0
            sm.Get("a3").ShouldBe("1.3333334");
        }


        [Fact]
        public void Set_Get_FormulaWithOnlyCellValues_complex()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a1", "20");
            sm.Set("a2", "15");
            sm.Set("a5", "=a1+a2");
            sm.Get("a5").ShouldBe("35");

            sm.Set("a5", "=a1+a2-30");
            sm.Get("a5").ShouldBe("5");

            sm.Set("a3", "1");
            sm.Set("a5", "=a1+a2-30*a3");
            sm.Get("a5").ShouldBe("5");

            sm.Set("a4", "2");
            sm.Set("a5", "=a1+a2-30*a3/a4");
            sm.Get("a5").ShouldBe("2.5");
        }
        [Fact]
        public void Set_Get_DivideByZero()
        {
            var sm = new SpreadSheetManager();
            sm.Set("a1", "20");
            sm.Set("a2", "15");
            sm.Set("a5", "=a1+a2/0");
            sm.Get("a5").ShouldBe("invalid-operation-division-by-zero");
        }
    }
}