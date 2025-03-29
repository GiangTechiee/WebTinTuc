using Microsoft.EntityFrameworkCore;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Data
{
    public class WebTinTucContext : DbContext
    {
        public WebTinTucContext(DbContextOptions<WebTinTucContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Quan hệ N-N giữa News và Category
            modelBuilder.Entity<News>()
                .HasMany(n => n.Categories)
                .WithMany(c => c.News)
                .UsingEntity(j => j.ToTable("NewsCategory"));

            // Quan hệ 1-N giữa User và Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.Fk_UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ 1-N giữa News và Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.News)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.Fk_NewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Replies)
                .WithOne(c => c.ParentComment)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            // quan hệ 1-N giữa User và News
            modelBuilder.Entity<News>()
                .HasOne(n => n.User)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.Fk_UserId)
                .OnDelete(DeleteBehavior.Restrict); // Hoặc Cascade tùy yêu cầu

            // Cấu hình composite primary key cho Favorite
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.Fk_UserId, f.Fk_NewId });

        }
    }
}
