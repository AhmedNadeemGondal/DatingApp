using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}

// Old method of writing traditional, explicit constructor
// public class AppDbContext : DbContext
// {
//     public AppDbContext(DbContextOptions options) : base(options) { }

//     public DbSet<AppUser> Users { get; set; }
// }