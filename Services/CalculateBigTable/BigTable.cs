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
    public decimal CalculateBigTable(FinancialDataInput input, FinancialDbContext context)
    {
        var balance = input.Balance;

        var loans = new List<Loan>();


        var databaseInputs = context.DatabaseInputs.FirstOrDefault();
        var months = new Month();

        foreach (var _month in months.Months)
        {
            var loan = new Loan
            {
                BeginningBalance = balance
            };

            var calculate = new CalculatedInputData(input, context);
            var calculatedInputs = calculate.CalculateAllBeforeLastTable(input, context);

            var paymentAmountCalculator = new PaymentAmount();
            var calculatedPaymentAmount = paymentAmountCalculator.CalculatePaymentAmount(
                calculatedInputs.UsedPayment, 
                months.Months[_month],
                databaseInputs.MaintenanceRate,
                input.InterestType,
                input.CommitmentAmount,
                input.MonthlyFeeIncome
                );

            var contractualInterestCalculator = new ContractualInterest();
            var calculatedContractualInterest = contractualInterestCalculator.CalculateContractualInterest(
                input.PaymentType,
                calculatedPaymentAmount,
                calculatedInputs.InterestRate,
                input.InterestSpread,
                input.CommitmentAmount,
                input.MonthlyFeeIncome
            );
            

            var contractualPrincipalCalculator = new ContractualPrincipal();
            var calculatedContractualPrincipal = contractualPrincipalCalculator.CalculateContractualPrincipal(
                calculatedContractualInterest,
                balance,
                calculatedPaymentAmount
            );

            
            var BaloonPaymentAtMaturityCalculator = new BaloonPaymentAtMaturity();
            var CalculatedBaloonPaymentAtMaturity = BaloonPaymentAtMaturityCalculator.CalculateBaloonPaymentAtMaturity(
                months.Months[_month],
                input.OriginalTermInMonths,
                loan.BeginningBalance
            );


            var TotalContractualCashFlowCalculator = new TotalContractualCashFlow();
            var CalculatedTotalContractualCashFlowy = TotalContractualCashFlowCalculator.CalculateTotalContractualCashFlow(
                loan.ContractualPrincipal,
                loan.BalloonPaymentAtMaturity
            );

            

            var PrepaymentCashflowCalculator = new PrepaymentCashflow();
            var CalculatedPrepaymentCashflowCalculator = PrepaymentCashflowCalculator.CalculatePrepaymentCashflow(
                loan.BeginningBalance,
                databaseInputs.PrepaymentRate,
                loan.TotalContractualCashflow
            );
            

            var TotalPrincipalPaidCalculator = new TotalPrincipalPaid();
            var CalculatedTotalPrincipalPaid = TotalPrincipalPaidCalculator.CalculateTotalPrincipalPaid(
                calculatedInputs.CapitalAllocationRate,
                CalculatedPrepaymentCashflowCalculator,
                CalculatedTotalContractualCashFlowy
            );

            var AnnualaizedInterestOnCashFlowCalculator = new AnnualaizedInterestOnCashFlow();
            var CalculatedAnnualaizedInterestOnCashFlow = AnnualaizedInterestOnCashFlowCalculator.AnnualaizedInterestOnCashFlowCalulate(
                CalculatedTotalPrincipalPaid,
                calculatedInputs.InterestRate
            );



        }




        return 2;


        // return ;
    }
}