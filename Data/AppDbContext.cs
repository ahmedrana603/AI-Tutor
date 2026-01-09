using AITutorWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace AITutorWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AIImage> AIImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AIImage>().HasData(
                new AIImage { Id = 1, Prompt = "Beautiful bird in nature", ImageGenerator = "Unsplash", UploadDate = DateTime.UtcNow.AddDays(-2), Filename = "https://images.unsplash.com/photo-1555169062-013468b47731?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGJpcmR8ZW58MHx8MHx8fDA%3D", Like = 10, canIncreaseLike = true, UserId = "demo@example.com", UserName = "Demo User" },
                new AIImage { Id = 2, Prompt = "Stunning landscape view", ImageGenerator = "Unsplash", UploadDate = DateTime.UtcNow.AddDays(-1), Filename = "https://images.unsplash.com/photo-1759405095263-b0413c67e57f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxmZWF0dXJlZC1waG90b3MtZmVlZHwyfHx8ZW58MHx8fHx8", Like = 7, canIncreaseLike = true, UserId = "demo@example.com", UserName = "Demo User" }
            );
        }
    }
}
