using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class UsedPaymentCalculator
{
    private readonly TransactionCostRateCalculator _transactionCostRateCalculator;

    public UsedPaymentCalculator(TransactionCostRateCalculator transactionCostRateCalculator)
    {
        _transactionCostRateCalculator = transactionCostRateCalculator;
    }

    public decimal CalculateUsedPayment(FinancialDataInput input)
    {
        decimal usedPayment;

        if (input.InterestType == "Fixed")
            usedPayment = input.Balance * input.InterestSpread;
        else
            usedPayment = input.Balance * input.TeaserSpread;

        var transactionCostRate = _transactionCostRateCalculator.CalculateTransactionCostRate(input);
        usedPayment += transactionCostRate;

        return usedPayment;
    }
}