using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Data;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportDbContext _dbContext;

        public ReportsController(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _dbContext.Reports.ToListAsync();
            return Ok(reports);
        }
    }
}
