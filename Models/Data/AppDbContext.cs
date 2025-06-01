using Microsoft.EntityFrameworkCore;
using RealTimeChatApp2025.Models;

namespace RealTimeChatApp2025.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
