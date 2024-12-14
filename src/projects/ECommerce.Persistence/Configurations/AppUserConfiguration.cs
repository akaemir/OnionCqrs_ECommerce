using Core.Security.Hashing;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Email).HasColumnName("Email").IsRequired();
        builder.Property(a => a.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(a => a.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(a => a.Status).HasColumnName("Status").HasDefaultValue(true);
        builder.Property(a => a.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(a => a.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(a => a.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
    private HashSet<AppUser> getData()
    {
        HashSet<AppUser> users = new HashSet<AppUser>();
        HashingHelper.CreatePasswordHash(
            password:"Password123",
            passwordSalt: out byte[] passwordSalt,
            passwordHash: out byte[] passwordHash
        );

        AppUser adminUser = new()
        {
            Id = 1,
            FirstName = "Ömer Doğan",
            LastName = "En iyi developer",
            Email = "admin@email.com",
        };
        return users;
    }
}