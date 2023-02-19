using Pricing_Engine.Models;

namespace Pricing_Engine.Services
{
    public class TransactionCostRateCalculator
    {
        public decimal CalculateTransactionCostRate(FinancialDataInput input)
        {
            var transactionCostRate = input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);

            return transactionCostRate;
        }
    }
}
