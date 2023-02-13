namespace Pricing_Engine.Models
{
    public class LoanAmortizationSchedule
    {
        public int Id { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ContractualInterest { get; set; }
        public decimal ContractualPrincipal { get; set; }
        public decimal BaloonPaymentAtMaturity { get; set; }
        public decimal TotalContractualCashflow { get; set; }
        public decimal PrepaymentCashflow { get; set; }
        public decimal TotalPrincipalPaid { get; set; }
        public decimal AnnaulizedInterestOnCashflow { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal EndingBalance { get; set; }
    }
}
