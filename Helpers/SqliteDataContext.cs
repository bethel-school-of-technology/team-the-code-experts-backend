using Microsoft.EntityFrameworkCore;
using Broadcast_JWT.Models;
namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;

public class SqliteDataContext : DataContext
{
    public SqliteDataContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<Broadcast_JWT.Models.Vote> Vote { get; set; }

    public DbSet<Broadcast_JWT.Models.Message> Message { get; set; }
}