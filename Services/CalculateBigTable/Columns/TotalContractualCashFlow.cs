namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class TotalContractualCashFlow
{
    public decimal CalculateTotalContractualCashFlow(
        decimal contractualPrincipal,
        decimal ballonPaymentAtMaturity)
    {
        return contractualPrincipal + ballonPaymentAtMaturity;
    }
}