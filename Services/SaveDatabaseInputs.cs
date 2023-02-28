using Microsoft.EntityFrameworkCore;
using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class SaveDatabaseInputs
{
    public void SaveTableToDatabase()
    {
        using var context = new FinancialDbContext(CreateDbContextOptions());

        var dbInputs = new DatabaseInputs
        {
            MaintenanceRate = 0.02M,
            PrepaymentRate = 0.07M,
            CreditRiskAllocation = "Capital",
            CapitalRiskRateWeight = 0.015
        };


        context.DatabaseInputs.Add(dbInputs);
        context.SaveChanges();
    }

    private static DbContextOptions<FinancialDbContext> CreateDbContextOptions()
    {
        var builder = new DbContextOptionsBuilder<FinancialDbContext>();
        builder.UseSqlServer("Server=localhost;Database=Financial_db;User Id=nika;Password=123;Encrypt=false;");
        return builder.Options;
    }
}