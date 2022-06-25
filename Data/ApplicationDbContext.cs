using Broadcast.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Broadcast.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Message> Messages { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Message>()
            .HasOne(c => c.User)
            .WithMany(t => t.Messages);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FollowingUser>()
            .HasOne(c => c.User)
            .WithMany(q => q.FollowingUsers);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vote>()
            .HasOne(c => c.Message)
            .WithMany(q => q.Votes);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Flag>()
            .HasOne(c => c.Message)
            .WithMany(q => q.Flags);
    }
    

    // public DbSet<Broadcast.Models.Vote> Vote { get; set; }
}

