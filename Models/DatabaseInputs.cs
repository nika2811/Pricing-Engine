namespace Pricing_Engine.Models
{
    public class DatabaseInputs
    {
        public decimal MaintenanceRate { get; set; }
        public decimal PrepaymentRate { get; set; }
        public string CreditRiskAllocation { get; set; }
        public decimal CapitalRiskRateWeight { get; set; }
    }
}
