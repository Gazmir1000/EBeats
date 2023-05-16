using EBeats.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeats.Data;

public class EBeatsContext : IdentityDbContext<IdentityUser>
{
    public EBeatsContext(DbContextOptions<EBeatsContext> options)
        : base(options)
    {

    }
    public DbSet<ApplicationUser>? Users { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Shipping>? Shippings { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

internal class ApplicationEnityUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Firstname).IsRequired();
        builder.Property(u => u.Lastname).IsRequired();
        builder.Property(u => u.Birthday).IsRequired();

    }
}
