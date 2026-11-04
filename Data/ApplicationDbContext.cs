using MedicalDeviceSafetyPlatform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalDeviceSafetyPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<SafetyNotice> SafetyNotices => Set<SafetyNotice>();
    public DbSet<Finding> Findings => Set<Finding>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        PatrickFindingModelConfiguration.ConfigurePatrickFindingModels(builder);
        builder.Entity<SafetyNotice>().HasIndex(n => n.Reference).IsUnique();
        // SQLite does not generate SQL Server rowversion values.
        // FindingsController sets a fresh token when creating/updating.
        builder.Entity<Finding>().Property(f => f.RowVersion)
            .IsConcurrencyToken().ValueGeneratedNever();
    }
}
