using System;
using System.Collections.Generic;

namespace ContactService.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public ICollection<ContactInfo> Infos { get; set; }
    }
}