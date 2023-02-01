using ContactAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.DbContexts
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }        

    }
}
