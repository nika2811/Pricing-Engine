using Microsoft.AspNetCore.Mvc;
using Pricing_Engine.Db;
using Pricing_Engine.Models;
using Pricing_Engine.Services;
using Pricing_Engine.Services.CalculateBigTable;

namespace Pricing_Engine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly FinancialDbContext _context;
    private readonly SaveDatabaseInputs _saveDatabaseInputs;


    public ValuesController(FinancialDbContext context, SaveDatabaseInputs saveDatabaseInputs)
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
        var calculatedInputs = new CalculatedInputData(input, _context);
        var interestRate = calculatedInputs.CalculateInterestRate();
        return Ok(interestRate);
    }

    [Route("api/Calculate-Transaction-cost-rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateTransactionCostRate(FinancialDataInput input)
    {
        var calculatedInputs = new CalculatedInputData(input, _context);
        var transactionCostRate = calculatedInputs.CalculateTransactionCostRate();
        return Ok(transactionCostRate);
    }


    [Route("api/Calculate-Capital-Allocation-Rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateCapitalAllocationRate(FinancialDataInput input)
    {
        var calculatedInputs = new CalculatedInputData(input, _context);
        var capitalAllocationRate = calculatedInputs.CalculateCapitalAllocationRate();
        return Ok(capitalAllocationRate);
    }

    [Route("api/Calculate-Used-Payment")]
    [HttpPost]
    public ActionResult<decimal> CalculateUsedPayment(FinancialDataInput input)
    {
        var calculatedInputs = new CalculatedInputData(input, _context);
        var usedPayment = calculatedInputs.CalculateUsedPayment();
        return Ok(usedPayment);
    }


    [Route("api/Calculate-Payment-Amount")]
    [HttpPost]
    public ActionResult<decimal> CalculateBigTable(FinancialDataInput input)
    {
        var bigTable = new BigTable();
        var calculatedPaymentAmount = bigTable.CalculateBigTable(
            input,
            _context
        );

        return Ok(calculatedPaymentAmount);
    }
}