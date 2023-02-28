using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class InterestRateCalculator
{
    public decimal CalculateInterestRate(FinancialDataInput input)
    {
        decimal interestRate;
        if (input is { InterestType: "Fixed", ProductType: "Loan" or "CD" })
            interestRate = input.InterestRate;
        else if (input.TeaserPeriod == 0)
            interestRate = input.TeaserSpread;
        else
            interestRate = input.InterestSpread + input.TeaserSpread;

        return interestRate;
    }
}