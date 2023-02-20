using Pricing_Engine.Models;

namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class TotalPrincipalPaid
    {
        public decimal CalculateTotalPrincipalPaid(
            decimal capitalAllocationRate,
            decimal prepaymentCashflow,
            decimal totalContractualCashflow
            )
        {
            var totalPrincipalPaid = totalContractualCashflow + prepaymentCashflow +
                                       (prepaymentCashflow * capitalAllocationRate);

            return totalPrincipalPaid;
        }
    }
}
