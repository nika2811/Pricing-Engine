namespace Pricing_Engine.Models
{
    public class Loan
    {

        public decimal PaymentAmount { get; set; }
        public decimal ContractualInterest { get; set; }
        public decimal ContractualPrincipal { get; set; }
        public decimal BalloonPaymentAtMaturity { get; set; }
        public decimal TotalContractualCashflow { get; set; }
        public decimal PrepaymentCashflow { get; set; }
        public decimal TotalPrincipalPaid { get; set; }
        public decimal AnnualizedInterestOnCashFlow { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal EndingBalance { get; set; }

    }
}
