using ContactList.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
