namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class BaloonPaymentAtMaturity
    {
        public decimal CalculateBaloonPaymentAtMaturity
            (int month, int OriginalTermInMonths, decimal beginingBalance)
        {
            if (month >= OriginalTermInMonths)
            {
                return -beginingBalance;
            }
            else
            {
                return 0;
            }
        }
    }
}
