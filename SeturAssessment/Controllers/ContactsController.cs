using ContactService.Models;
using ContactService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel contact)
        {
            var created = await _service.CreateAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPost("{contactId}/infos")]
        public async Task<IActionResult> AddInfo(Guid contactId, ContactInfoModel info)
        {
            var added = await _service.AddContactInfoAsync(contactId, info);
            return Ok(added);
        }

        [HttpDelete("{contactId}/infos/{infoId}")]
        public async Task<IActionResult> DeleteInfo(Guid contactId, Guid infoId)
        {
            var result = await _service.DeleteContactInfoAsync(contactId, infoId);
            return result ? NoContent() : NotFound();
        }
    }
}
