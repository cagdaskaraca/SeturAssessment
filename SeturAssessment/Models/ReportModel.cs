using System;

namespace ReportService.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
