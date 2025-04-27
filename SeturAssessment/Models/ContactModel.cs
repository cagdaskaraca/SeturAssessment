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
        public string Email { get; set; }

        public ICollection<ContactInfoModel> Infos { get; set; }
    }
}