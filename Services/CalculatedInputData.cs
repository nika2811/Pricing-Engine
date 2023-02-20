using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class CalculatedInputData
{
    private readonly FinancialDbContext _context;
    private readonly FinancialDataInput _input;
    private readonly TransactionCostRateCalculator _transactionCostRateCalculator;

    public CalculatedInputData(FinancialDataInput input, FinancialDbContext context)
    {
        _input = input;
        _context = context;
        _transactionCostRateCalculator = new TransactionCostRateCalculator();
    }

    public decimal CalculateInterestRate()
    {
        var calculator = new InterestRateCalculator();
        var interestRate = calculator.CalculateInterestRate(_input);
        return interestRate;
    }

    public decimal CalculateTransactionCostRate()
    {
        var transactionCostRate = _transactionCostRateCalculator.CalculateTransactionCostRate(_input);
        return transactionCostRate;
    }

    public decimal CalculateCapitalAllocationRate()
    {
        var calculator = new CapitalAllocationRateCalculator(_context);
        var capitalAllocationRate = calculator.Calculate();
        return capitalAllocationRate;
    }

    public decimal CalculateUsedPayment()
    {
        var calculator = new UsedPaymentCalculator(_transactionCostRateCalculator);
        var usedPayment = calculator.CalculateUsedPayment(_input);
        return usedPayment;
    }


    public CalculatedInputs CalculateAllBeforeLastTable(FinancialDataInput input, FinancialDbContext context)
    {
        var calculatedInputs = new CalculatedInputs();

        var interestRateCalculator = new InterestRateCalculator();
        var interestRate = interestRateCalculator.CalculateInterestRate(input);
        calculatedInputs.InterestRate = interestRate;

        var transactionCostRateCalculator = new TransactionCostRateCalculator();
        var transactionCostRate = transactionCostRateCalculator.CalculateTransactionCostRate(input);
        calculatedInputs.TransactionCostRate = transactionCostRate;

        var capitalAllocationRateCalculator = new CapitalAllocationRateCalculator(context);
        var capitalAllocationRate = capitalAllocationRateCalculator.Calculate();
        calculatedInputs.CapitalAllocationRate = capitalAllocationRate;

        var usedPaymentCalculator = new UsedPaymentCalculator(transactionCostRateCalculator);
        var usedPayment = usedPaymentCalculator.CalculateUsedPayment(input);
        calculatedInputs.UsedPayment = usedPayment;
        return calculatedInputs;
    }
}