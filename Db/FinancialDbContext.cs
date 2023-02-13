﻿using Microsoft.EntityFrameworkCore;
using Pricing_Engine.Models;

namespace Pricing_Engine.Db;

public class FinancialDbContext : DbContext
{
    public FinancialDbContext(DbContextOptions<FinancialDbContext> options) : base(options)
    {
    }

    public DbSet<FinancialDataInput> Finance { get; set; }
}