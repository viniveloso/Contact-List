using ContactList.Application.Dtos;
using ContactList.Application.UseCases;
using ContactList.Application.Interfaces;
using ContactList.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        private readonly CreateContactUseCase _createContactUseCase;
        private readonly UpdateContactUseCase _updateContactUseCase;
        private readonly GetAddressDetailsByCepQuery _getAddressDetailsByCepQuery;

        public ContactController(IContactRepository repository, CreateContactUseCase createContactUseCase, UpdateContactUseCase updateContactUseCase, GetAddressDetailsByCepQuery getAddressDetailsByCepQuery)
        {
            _repository = repository;
            _createContactUseCase = createContactUseCase;
            _updateContactUseCase = updateContactUseCase;
            _getAddressDetailsByCepQuery = getAddressDetailsByCepQuery;
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

            var result = await _createContactUseCase.ExecuteAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return CreatedAtRoute("GetContactById", new { id = result.Contact!.Id }, result.Contact);
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
            existing.Cep = dto.Cep;

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

        [HttpGet("address-by-cep/{cep}")]
        [ProducesResponseType(typeof(AddressDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddressByCep(string cep)
        {
            var addressDetails = await _getAddressDetailsByCepQuery.ExecuteAsync(cep);

            if (addressDetails == null)
            {
                return NotFound("CEP não encontrado ou formato inválido.");
            }

            return Ok(addressDetails);
        }
    }
}
