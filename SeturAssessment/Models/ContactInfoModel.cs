using System;

namespace ContactService.Models
{
    public class ContactInfoModel
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public InfoType Type { get; set; }
        public string Content { get; set; }

        public Contact Contact { get; set; }
    }
}