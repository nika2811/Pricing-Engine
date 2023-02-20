namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class ContractualPrincipal
    {
        public decimal CalculateContractualPrincipal(decimal contractualInterest, decimal beginingBalance, decimal paymentAmount)
        {
            var overdueInterest = - (paymentAmount - contractualInterest);

            if (overdueInterest > beginingBalance)
            {
                overdueInterest = -beginingBalance;
            }
            else
            {
                overdueInterest = Math.Min(0, - overdueInterest);
            }

            return overdueInterest;
        }

    }
}
