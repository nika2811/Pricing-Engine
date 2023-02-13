using Microsoft.EntityFrameworkCore;
using Pricing_Engine.Models;

namespace Pricing_Engine.Db;

public class FinancialDbContext : DbContext
{
    public FinancialDbContext(DbContextOptions<FinancialDbContext> options) : base(options)
    {
    }

    public DbSet<FinancialDataInput> Finance { get; set; }
    public DbSet<DatabaseInputs> DatabaseInputs { get; set; }
    public DbSet<CalculatedInputs> CalculatedInputs { get; set; }
    public DbSet<LoanAmortizationSchedule> LoanAmortizationSchedules { get;set; }

}