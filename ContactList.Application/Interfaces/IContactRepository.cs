using ContactList.Domain.Entities;

namespace ContactList.Application.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
