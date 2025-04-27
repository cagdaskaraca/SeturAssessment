using Microsoft.EntityFrameworkCore;
using ReportService.Models;

namespace SeturAssessment.Data
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

        public DbSet<ReportModel> Reports { get; set; }
    }
}
