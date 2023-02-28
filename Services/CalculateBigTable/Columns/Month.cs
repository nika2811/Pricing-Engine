namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class Month
{
    public Month()
    {
        Months = Enumerable.Range(2, 13).ToArray();
    }

    public int[] Months { get; set; }
}