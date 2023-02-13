using Microsoft.AspNetCore.Mvc;
using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly FinancialDbContext _context;

    public ValuesController(FinancialDbContext context)
    {
        _context = context;
    }

    [Route("api/Calculate-Interest-Rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateInterestRate(FinancialDataInput input)
    {
        decimal interestRate;
        if (input.InterestType == "Fixed" && (input.ProductType == "Loan" || input.ProductType == "CD"))
            interestRate = input.InterestRate;
        else if (input.TeaserPeriod == 0)
            interestRate = input.InterestRate;
        else
            interestRate = input.InterestSpread + input.InterestRate;

        return Ok(interestRate);
    }

    [Route("api/Calculate-Transaction-cost-rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateTransactionCostRate(FinancialDataInput input)
    {
        decimal transactionCostRate = input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);
        return Ok(transactionCostRate);
    }


}