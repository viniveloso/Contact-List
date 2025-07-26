using ContactList.Application.Interfaces;
using ContactList.Domain.Entities;
using ContactList.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ContactList.Domain.Entities;
using ContactList.Application.Interfaces;
using ContactList.Infrastructure.Persistence;

namespace ContactList.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return false;
            }

            _context.Contacts.Remove(contact);
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        { 
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                return contact;
            } else
            {
                throw new ArgumentException("Contact not found.");
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            var existing = await _context.Contacts.FindAsync(contact.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(contact);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
