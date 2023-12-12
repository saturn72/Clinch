// See https://aka.ms/new-console-template for more information
using ClinchSpreadsheet;

Console.WriteLine("Hello, World!");

var ssm = new SpreadSheetManager();
ssm.Set("A1", "20");
ssm.Set("A2", "25");
ssm.Set("A3", "=A1+A2");

var a3 = ssm.Get("A3");
Console.WriteLine("A3 value is: " + a3);

ssm.Set("A1", "15");
ssm.Set("A3", "=A1*A2");

a3 = ssm.Get("A3");
Console.WriteLine("A3 value is now: " + a3);

ssm.Set("A3", "=A1/A2");
a3 = ssm.Get("A3");
Console.WriteLine("A3 value is now: " + a3);

Console.WriteLine("Press any key to continue..." );
Console.ReadKey();
