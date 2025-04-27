using SeturAssessment.Data;
using ContactService.Models;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeturAssessment.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactDbContext _context;

        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<ContactModel> CreateAsync(ContactModel contact)
        {
            contact.Id = Guid.NewGuid();
            _context.ContactModel.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var contact = await _context.ContactModel.FindAsync(id);
            if (contact == null) return false;

            _context.ContactModel.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ContactModel>> GetAllAsync() => await _context.ContactModel.Include(c => c.Infos).ToListAsync();

        public async Task<ContactModel> GetByIdAsync(Guid id) => await _context.ContactModel.Include(c => c.Infos).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<ContactInfoModel> AddContactInfoAsync(Guid contactId, ContactInfoModel info)
        {
            info.Id = Guid.NewGuid();
            info.ContactId = contactId;
            _context.ContactInfoModel.Add(info);
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<bool> DeleteContactInfoAsync(Guid contactId, Guid infoId)
        {
            var info = await _context.ContactInfoModel.FirstOrDefaultAsync(i => i.ContactId == contactId && i.Id == infoId);
            if (info == null) return false;

            _context.ContactInfoModel.Remove(info);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ContactModel>> GetAll()
        {
            return await _context.ContactModel.ToListAsync();
        }

    }
}