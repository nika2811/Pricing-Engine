using Microsoft.AspNetCore.Mvc;
using Pricing_Engine.Db;
using Pricing_Engine.Models;
using Pricing_Engine.Services;

namespace Pricing_Engine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly CalculatedInputs _calculatedInputs = new CalculatedInputs();

    private readonly FinancialDbContext _context;
    private readonly SaveDatabaseInputs _saveDatabaseInputs;

    public ValuesController(FinancialDbContext context,SaveDatabaseInputs saveDatabaseInputs)
    {
        _context = context;
        _saveDatabaseInputs = saveDatabaseInputs;
    }

    [Route("api/Save-Database-Inputs")]
    [HttpPost]
    public IActionResult SaveTable()
    {
        _saveDatabaseInputs.SaveTableToDatabase();
        return Ok("Table information saved successfully.");
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

        _calculatedInputs.InterestRate = interestRate;

        return Ok(interestRate);
    }

    [Route("api/Calculate-Transaction-cost-rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateTransactionCostRate(FinancialDataInput input)
    {
        var transactionCostRate = input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);

        _calculatedInputs.TransactionCostRate = transactionCostRate;

        return Ok(transactionCostRate);
    }


    [Route("api/Calculate-Capital-Allocation-Rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateCapitalAllocationRate()
    {
        var databaseInputs =_context.DatabaseInputs.FirstOrDefault() ;
        decimal capitalAllocationRate = 0;
        if (databaseInputs.CreditRiskAllocation == "Capital")
            capitalAllocationRate = databaseInputs.CapitalRiskRateWeight + databaseInputs.MaintenanceRate;
        else
            capitalAllocationRate = databaseInputs.MaintenanceRate;
    
        _calculatedInputs.CapitalAllocationRate = capitalAllocationRate;
    
        return Ok(capitalAllocationRate);
    }

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

        _calculatedInputs.UsedPayment = usedPayment;

        return usedPayment;
    }
}