using Microsoft.AspNetCore.Mvc;
using Pricing_Engine.Db;
using Pricing_Engine.Models;

namespace Pricing_Engine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly FinancialDbContext _context;

    public ValuesController(FinancialDbContext context)
    {
        _context = context;
    }

   
}