namespace WebApi.Helpers;

using Broadcast_JWT.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    // public DbSet<Message> Messages { get; set; }
    public DbSet<FollowingUser> FollowingUsers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<FlagType> FlagTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Message>()
            .HasOne(c => c.AppUser)
            .WithMany(t => t.Messages);

        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<FollowingUser>()
        //     .HasOne(c => c.AppUser)
        //     .WithMany(q => q.FollowingUsers);

        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Vote>()
        //     .HasOne(c =>c.Message)
        //     .WithMany(q => q.Votes);

        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Flag>()
        //     .HasOne(c => c.Message)
        //     .WithMany(q => q.Flags);


    }

}