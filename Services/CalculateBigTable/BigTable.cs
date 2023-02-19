using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Services.CalculateBigTable;

public class BigTable
{
    public void CalculateBigTable(FinancialDataInput input, FinancialDbContext context)
    {
        var calculate = new CalculatedInputData(input, context);
        var CalculatedInputs = calculate.CalculateAllBeforeLastTable(input, context);
    }
}