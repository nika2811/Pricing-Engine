namespace Pricing_Engine.Models
{
    public class CalculatedInputs
    {
        public int Id { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TransactionCostRate { get; set; }
        public decimal CapitalAllocationRate { get; set; }
        public decimal UsedPayment { get; set; }
    }
}
