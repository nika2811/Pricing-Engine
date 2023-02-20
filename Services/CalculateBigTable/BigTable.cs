using Pricing_Engine.Db;
using Pricing_Engine.Models;
using Pricing_Engine.Services.CalculateBigTable.Columns;

namespace Pricing_Engine.Services.CalculateBigTable;

public class BigTable
{
    private readonly FinancialDbContext _context;

    public BigTable(FinancialDbContext context)
    {
        _context = context;
    }
    public decimal CalculateBigTable(FinancialDataInput input, FinancialDbContext context,int month)
    {
        var databaseInputs = context.DatabaseInputs.FirstOrDefault();
        var months = new Month();

        var calculate = new CalculatedInputData(input, context);
        var calculatedInputs = calculate.CalculateAllBeforeLastTable(input, context);

        var paymentAmountCalculator = new PaymentAmount();
        var calculatedPaymentAmount = paymentAmountCalculator.CalculatePaymentAmount(
            calculatedInputs.UsedPayment, 
            months.Months[month],
            databaseInputs.MaintenanceRate,
            input.InterestType,
            input.CommitmentAmount,
            input.MonthlyFeeIncome
            );

        var contractualInterestCalculator = new ContractualInterest();
        var calculatedContractualInterest = contractualInterestCalculator.CalculateContractualInterest(
            input.PaymentType,
            calculatedPaymentAmount,
            input.InterestRate,
            input.InterestSpread,
            input.CommitmentAmount,
            input.MonthlyFeeIncome,
            month
        );









        return calculatedContractualInterest;
    }
}