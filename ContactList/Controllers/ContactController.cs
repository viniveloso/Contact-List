using ContactList.Application.Dtos;
using ContactList.Application.Interfaces;
using ContactList.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<ActionResult> GetById(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact != null)
            {
                return Ok(contact);
            } else
            {
                return NotFound("Contact not found.");
            }           
        }

        [HttpGet(Name = "GetContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAll()
        {
            var contacts = await _repository.GetAllAsync();
            return Ok(contacts); //Código Http 200
        }

        [HttpPost(Name = "PostContact")]
        public async Task<ActionResult> PostContact([FromBody] CreateContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _repository.AddAsync(contact);

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetContactById", new { id = contact.Id }, contact);
        }

        [HttpPut("{id}", Name = "PutContact")]
        public async Task<IActionResult> PutContact(int id, [FromBody] UpdateContactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Contact not found.");

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Address = dto.Address;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return Ok("Contact changed.");
        }

        [HttpDelete("{id}", Name = "DeleteContact")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound("Contact not found.");

            await _repository.SaveChangesAsync();
            return Ok("Contact removed.");
        }
    }
}
