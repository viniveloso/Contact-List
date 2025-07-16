using ContactList.Models;
using Microsoft.AspNetCore.Mvc;
using ContactList.Dtos;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        [HttpGet("{id}", Name = "GetContactById")]
        public Contact GetById(int id)
        {
            return contacts.Where(c => c.Id == id).FirstOrDefault();
        }

        [HttpGet(Name = "GetContacts")]
        public List<Contact> GetAll()
        {
            return contacts;
        }

        [HttpPost(Name = "PostContact")]
        public IActionResult PostContact([FromBody] CreateContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact
            {
                Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
            };

            contacts.Add(contact);

            return Ok("Contato adicionado");
        }

        [HttpPut("{id}", Name = "PutContact")]
        public IActionResult PutContact(int id, [FromBody] UpdateContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int index = contacts.FindIndex(c => c.Id == id);

            if (index == -1)
            {
                return NotFound("Contato não encontrado");
            }

            contacts[index].Name = dto.Name;
            contacts[index].Email = dto.Email;
            contacts[index].Phone = dto.Phone;
            contacts[index].Address = dto.Address;

            return Ok($"Contato alterado");
        }

        [HttpDelete(Name = "DeleteContact")]
        public IActionResult DeleteContact(int id)
        {
            var contato = contacts.Find(c => c.Id == id);
            contacts.Remove(contato);
            return Ok($"Contato removido");
        }

        

        public static List<Contact> contacts = new List<Contact>() {
            
            new Contact()
            {
                Id = 1,
                Address = "Rua Cachoeira de Minas, 546",
                Email = "erick.araujo98@hotmail.com",
                Name = "Erick",
                Phone = "(11) 97444-2088"
            },
            new Contact()
            {
                Id = 2,
                Address = "Rua Antonio amansio",
                Email = "gordao98@hotmail.com",
                Name = "gordao",
                Phone = "(11) 97422-2088"
            },
            new Contact()
            {
                Id = 3,
                Address = "rua penelope souza, 133",
                Email = "cris.greg86@hotmail.com",
                Name = "crisogreg",
                Phone = "(11) 94754-2088"
            }

        };
    }
}
