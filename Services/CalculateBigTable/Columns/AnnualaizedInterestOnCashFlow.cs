namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class AnnualaizedInterestOnCashFlow
    {
        public decimal AnnualaizedInterestOnCashFlowCalulate(decimal totalPrincipalPaid,decimal interestRate)
        {
            return totalPrincipalPaid + interestRate;
        }
    }
}
