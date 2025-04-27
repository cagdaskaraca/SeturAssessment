using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportService.Models;
using SeturAssessment.Data;
using SeturAssessment.Data.ReportService.Data;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] ReportModel report)
        {
            report.Id = Guid.NewGuid();
            report.CreatedAt = DateTime.UtcNow;
            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReports), new { id = report.Id }, report);
        }
    }
}
