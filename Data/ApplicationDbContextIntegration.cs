using MedicalDeviceSafetyPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDeviceSafetyPlatform.Data;

/* Add these sets and mappings to the application's existing DbContext. */

// public DbSet<Finding> Findings => Set<Finding>();
// public DbSet<Comment> Comments => Set<Comment>();

public static class PatrickFindingModelConfiguration
{
    public static void ConfigurePatrickFindingModels(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Finding>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Title).HasMaxLength(150).IsRequired();
            entity.Property(f => f.Analysis).HasMaxLength(4000).IsRequired();

            entity.HasOne(f => f.SafetyNotice)
                .WithMany() // Replace with .WithMany(n => n.Findings) if SafetyNotice exposes that navigation.
                .HasForeignKey(f => f.SafetyNoticeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Employee)
                .WithMany()
                .HasForeignKey(f => f.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Body).HasMaxLength(1500).IsRequired();

            entity.HasOne(c => c.Finding)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FindingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

/*
Inside ApplicationDbContext.OnModelCreating(ModelBuilder builder):

base.OnModelCreating(builder);
PatrickFindingModelConfiguration.ConfigurePatrickFindingModels(builder);
*/
