using Microsoft.EntityFrameworkCore;
 
namespace infertilityAndBeyondProject.Models
{
    public class myWebsiteContext : DbContext
    {
        public myWebsiteContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}