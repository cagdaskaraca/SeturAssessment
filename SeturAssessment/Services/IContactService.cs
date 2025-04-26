using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactService.Models;

namespace ContactService.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactModel>> GetAllAsync();
        Task<ContactModel> GetByIdAsync(Guid id);
        Task<ContactModel> CreateAsync(ContactModel contact);
        Task<bool> DeleteAsync(Guid id);

        Task<ContactInfoModel> AddContactInfoAsync(Guid contactId, ContactInfoModel info);
        Task<bool> DeleteContactInfoAsync(Guid contactId, Guid infoId);
    }
}