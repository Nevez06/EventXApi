using EventXFullApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventXFullApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        
    }
}
