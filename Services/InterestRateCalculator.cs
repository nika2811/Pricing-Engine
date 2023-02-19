using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class InterestRateCalculator
{
    public decimal CalculateInterestRate(FinancialDataInput input)
    {
        decimal interestRate;
        if (input.InterestType == "Fixed" && (input.ProductType == "Loan" || input.ProductType == "CD"))
            interestRate = input.InterestRate;
        else if (input.TeaserPeriod == 0)
            interestRate = input.TeaserSpread;
        else
            interestRate = input.InterestSpread + input.TeaserSpread;

        return interestRate;
    }
}