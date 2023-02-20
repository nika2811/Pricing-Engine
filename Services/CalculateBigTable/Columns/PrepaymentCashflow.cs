namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class PrepaymentCashflow
    {
        public decimal CalculatePrepaymentCashflow(decimal beginningBalance, decimal prePaymentCashRate,decimal totalContractualCashflow)
        {
            // if (-totalContractualCashflow >= beginningBalance)
            // {
            //     return 0;
            // }
            //
            // return Math.Max(
            //     -(beginningBalance + totalContractualCashflow),
            //     -prePaymentCashRate * beginningBalance
            // );
            var remainingBalance = beginningBalance + totalContractualCashflow; // calculate the remaining balance after subtracting the contractual cashflows

            // if the remaining balance is already negative, no prepayment cashflow is necessary
            if (remainingBalance < 0)
            {
                return 0;
            }

            var maximumPrepayment = prePaymentCashRate * beginningBalance; // calculate the maximum prepayment allowed based on the prepayment cash rate

            // the prepayment cashflow is the minimum between the remaining balance and the maximum prepayment allowed
            var prepaymentCashflow = Math.Min(remainingBalance, maximumPrepayment);

            return -prepaymentCashflow; // return the negative prepayment cashflow, as it represents an outflow of funds
        }

    }
}
