using Microsoft.AspNetCore.Mvc;
using Pricing_Engine.Db;
using Pricing_Engine.Models;
using Pricing_Engine.Services;

namespace Pricing_Engine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly CalculatedInputs _calculatedInputs = new();

    private readonly FinancialDbContext _context;
    private readonly SaveDatabaseInputs _saveDatabaseInputs;
    private readonly int[] months = Enumerable.Range(2, 13).ToArray();

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
        var calculator = new InterestRateCalculator();
        var interestRate = calculator.CalculateInterestRate(input);
        
        _calculatedInputs.InterestRate = interestRate;
        return Ok(interestRate);

    }

    [Route("api/Calculate-Transaction-cost-rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateTransactionCostRate(FinancialDataInput input)
    {
        var calculator = new TransactionCostRateCalculator();
        var transactionCostRate = calculator.CalculateTransactionCostRate(input);

        _calculatedInputs.TransactionCostRate = transactionCostRate;
        return Ok(transactionCostRate);
    }


    [Route("api/Calculate-Capital-Allocation-Rate")]
    [HttpPost]
    public ActionResult<decimal> CalculateCapitalAllocationRate()
    {
        var calculator = new CapitalAllocationRateCalculator(_context);
        var capitalAllocationRate = calculator.Calculate();

        _calculatedInputs.CapitalAllocationRate = capitalAllocationRate;

        return Ok(capitalAllocationRate);
    }

    [Route("api/Calculate-Used-Payment")]
    [HttpPost]
    public ActionResult<decimal> CalculateUsedPayment(FinancialDataInput input)
    {
        var calculator = new UsedPaymentCalculator(new TransactionCostRateCalculator());
        var usedPayment = calculator.CalculateUsedPayment(input);

        _calculatedInputs.UsedPayment = usedPayment;

        return Ok(usedPayment);
    }

    [Route("api/Calculate-Payment-Amount")]
    [HttpPost]
    public IActionResult CalculatePaymentAmount(FinancialDataInput input)
    {
        var interestType = input.InterestType;
        var commitmentAmount = input.CommitmentAmount;
        var monthlyFeeIncome = input.MonthlyFeeIncome;
        var maintenanceRate = _context.DatabaseInputs.FirstOrDefault().MaintenanceRate;
        var usedPayment = _calculatedInputs.UsedPayment;


        var paymentAmount = usedPayment * months[0] + usedPayment * maintenanceRate;
        ;
        if (interestType == "Variable")
            paymentAmount += commitmentAmount + monthlyFeeIncome;
        else
            paymentAmount += 0;

        return Ok(paymentAmount);
    }
}