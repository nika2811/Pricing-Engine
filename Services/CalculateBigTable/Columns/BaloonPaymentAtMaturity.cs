namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class BaloonPaymentAtMaturity
{
    public decimal CalculateBaloonPaymentAtMaturity
        (int month, int originalTermInMonths, decimal beginingBalance)
    {
        if (month >= originalTermInMonths)
            return -beginingBalance;
        return 0;
    }
}