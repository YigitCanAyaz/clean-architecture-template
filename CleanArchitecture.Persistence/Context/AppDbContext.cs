using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context;

public sealed class AppDbContext : IdentityDbContext<User, IdentityRole, string>
{
    //AppDbContext context = new();
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("");
    //}

    // best practice budur
    public AppDbContext(DbContextOptions options) : base(options)
    {
        // çağırmak istersek => private readonly AppDbContext _context, sonra constructor'dan eşleştir.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityRole<string>>();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
