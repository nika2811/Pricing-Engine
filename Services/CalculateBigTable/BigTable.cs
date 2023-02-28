using Pricing_Engine.Db;
using Pricing_Engine.Models;
using Pricing_Engine.Services.CalculateBigTable.Columns;

namespace Pricing_Engine.Services.CalculateBigTable;

public class BigTable
{
    public decimal CalculateBigTable(FinancialDataInput input, FinancialDbContext context)
    {
        var balance = input.Balance;

        var loans = new List<Loan>();


        var databaseInputs = context.DatabaseInputs.FirstOrDefault();
        var months = new Month();

        var calculate = new CalculatedInputData(input, context);
        var calculatedInputs = calculate.CalculateAllBeforeLastTable(input, context);

        decimal sumOfEndingBalance = 0;
        foreach (var month in months.Months)
        {
            var loan = new Loan
            {
                BeginningBalance = balance
            };


            var paymentAmountCalculator = new PaymentAmount();
            var calculatedPaymentAmount = paymentAmountCalculator.CalculatePaymentAmount(
                calculatedInputs.UsedPayment,
                months.Months[month - 2],
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


            var baloonPaymentAtMaturityCalculator = new BaloonPaymentAtMaturity();
            var calculatedBaloonPaymentAtMaturity = baloonPaymentAtMaturityCalculator.CalculateBaloonPaymentAtMaturity(
                months.Months[month - 2],
                input.OriginalTermInMonths,
                loan.BeginningBalance
            );


            var totalContractualCashFlowCalculator = new TotalContractualCashFlow();
            var calculatedTotalContractualCashFlowy =
                totalContractualCashFlowCalculator.CalculateTotalContractualCashFlow(
                    // loan.ContractualPrincipal,
                    calculatedContractualPrincipal,
                    calculatedBaloonPaymentAtMaturity
                );


            var prepaymentCashflowCalculator = new PrepaymentCashflow();
            var calculatedPrepaymentCashflowCalculator = prepaymentCashflowCalculator.CalculatePrepaymentCashflow(
                loan.BeginningBalance,
                databaseInputs.PrepaymentRate,
                calculatedTotalContractualCashFlowy
            );


            var totalPrincipalPaidCalculator = new TotalPrincipalPaid();
            var calculatedTotalPrincipalPaid = totalPrincipalPaidCalculator.CalculateTotalPrincipalPaid(
                calculatedInputs.CapitalAllocationRate,
                calculatedPrepaymentCashflowCalculator,
                calculatedTotalContractualCashFlowy
            );

            var annualaizedInterestOnCashFlowCalculator = new AnnualaizedInterestOnCashFlow();
            var calculatedAnnualaizedInterestOnCashFlow =
                AnnualaizedInterestOnCashFlow.AnnualaizedInterestOnCashFlowCalulate(
                    calculatedTotalPrincipalPaid,
                    calculatedInputs.InterestRate
                );


            loan.EndingBalance = calculatedTotalPrincipalPaid + loan.BeginningBalance;

            balance = loan.EndingBalance;

            sumOfEndingBalance += loan.EndingBalance;
            loans.Add(loan);
        }

        return sumOfEndingBalance;
    }
}