using API_Forms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API_Forms.Data
{
    public class AppDbContext: DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
