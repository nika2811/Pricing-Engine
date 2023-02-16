using Microsoft.EntityFrameworkCore;
using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Services;

public class SaveDatabaseInputs
{
    public void SaveTableToDatabase()
    {
        using var context = new FinancialDbContext(CreateDbContextOptions());
        // Create a new DatabaseInputs object and set its properties
        var dbInputs = new DatabaseInputs
        {
            MaintenanceRate = 0.02M,
            PrepaymentRate = 0.07M,
            CreditRiskAllocation = "Capital",
            CapitalRiskRateWeight = 0.015M
        };

        // Add the new object to the DatabaseInputs DbSet and save changes to the database
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