using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context;

public sealed class AppDbContext : DbContext
{
    //AppDbContext context = new();
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("");
    //}

    // best practice budur
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // çağırmak istersek => private readonly AppDbContext _context, sonra constructor'dan eşleştir.
}
