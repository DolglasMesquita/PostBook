using Microsoft.EntityFrameworkCore;
using PostBook.Domain.Entities;

namespace PostBook.Infraestructure.Data;

public class PostBookContext : DbContext
{
    public PostBookContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>().HasKey(l => new { l.PostId, l.UserId });
    }

}
