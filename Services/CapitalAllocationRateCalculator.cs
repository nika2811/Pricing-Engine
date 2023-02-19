using Pricing_Engine.Db;

namespace Pricing_Engine.Services
{
    public class CapitalAllocationRateCalculator
    {
        private readonly FinancialDbContext _context;

        public CapitalAllocationRateCalculator(FinancialDbContext context)
        {
            _context = context;
        }

        public decimal Calculate()
        {
            var databaseInputs = _context.DatabaseInputs.FirstOrDefault();
            decimal capitalAllocationRate;
            if (databaseInputs.CreditRiskAllocation == "Capital")
                capitalAllocationRate = (decimal)(databaseInputs.CapitalRiskRateWeight + (double)databaseInputs.MaintenanceRate);
            else
                capitalAllocationRate = databaseInputs.MaintenanceRate;

            return capitalAllocationRate;
        }
    }

}
