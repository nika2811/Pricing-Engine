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

        var record = _context.CalculatedInputs.FirstOrDefault(x => x.Id == 1);
        if (record != null)
        {
            record.InterestRate = interestRate;
            _context.SaveChanges();
        }

        return Ok(interestRate);
    }

    [Route("api/Calculate-Transaction-cost-rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateTransactionCostRate(FinancialDataInput input)
    {
        var transactionCostRate = input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);

        var record = _context.CalculatedInputs.FirstOrDefault(x => x.Id == 1);
        if (record != null)
        {
            record.TransactionCostRate = transactionCostRate;
            _context.SaveChanges();
        }

        return Ok(transactionCostRate);
    }


    [Route("api/Calculate-Capital-Allocation-Rate")]
    [HttpPost]
    // public ActionResult<decimal> CalculateCapitalAllocationRate()
    // {
    //     var databaseInputs = _context.;
    //     decimal capitalAllocationRate = 0;
    //     if (databaseInputs.CreditRiskAllocation == "Capital")
    //         capitalAllocationRate = databaseInputs.CapitalRiskRateWeight + databaseInputs.MaintenanceRate;
    //     else
    //         capitalAllocationRate = databaseInputs.MaintenanceRate;
    //
    //     var record = _context.CalculatedInputs.FirstOrDefault(x => x.Id == 1);
    //     if (record != null)
    //     {
    //         record.CapitalAllocationRate = capitalAllocationRate;
    //         _context.SaveChanges();
    //     }
    //
    //     return Ok(capitalAllocationRate);
    // }


    [Route("api/Calculate-Used-Payment")]
    [HttpPost]
    public ActionResult<decimal> CalculateUsedPayment(FinancialDataInput input)
    {
        decimal usedPayment = 0;
        if (input.InterestType == "Fixed")
            usedPayment = input.Balance * input.InterestSpread;
        else
            usedPayment = input.Balance * input.TeaserSpread;

        var transactionCostRate = CalculateTransactionCostRate(input).Value;
        usedPayment += transactionCostRate;

        var record = _context.CalculatedInputs.FirstOrDefault(x => x.Id == 1);
        if (record != null)
        {
            record.UsedPayment = usedPayment ;
            _context.SaveChanges();
        }

        return usedPayment;
    }
}