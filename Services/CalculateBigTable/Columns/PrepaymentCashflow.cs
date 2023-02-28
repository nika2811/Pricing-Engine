namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class PrepaymentCashflow
{
    public decimal CalculatePrepaymentCashflow(decimal beginningBalance, decimal prePaymentCashRate,
        decimal totalContractualCashflow)
    {
        var remainingBalance =
            beginningBalance +
            totalContractualCashflow;


        if (remainingBalance < 0) return 0;

        var maximumPrepayment =
            prePaymentCashRate *
            beginningBalance;

        var prepaymentCashflow = Math.Min(remainingBalance, maximumPrepayment);

        return -prepaymentCashflow;
    }
}