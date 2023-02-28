namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class AnnualaizedInterestOnCashFlow
{
    public static decimal AnnualaizedInterestOnCashFlowCalulate(decimal totalPrincipalPaid, decimal interestRate)
    {
        return totalPrincipalPaid + interestRate;
    }
}