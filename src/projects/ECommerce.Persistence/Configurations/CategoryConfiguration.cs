using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category").HasKey(x=>x.Id);
        builder.Property(x=>x.Id).HasColumnName("CategoryId");
        builder.Property(x=>x.Name).HasColumnName("Name");
        builder.Property(x=>x.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(x=>x.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(x=>x.DeletedDate).HasColumnName("DeletedDate");
        
        builder
            .HasMany(x=>x.SubCategories)
            .WithOne(c=>c.Category)
            .HasForeignKey(c=>c.CategoryId)
            .OnDelete(deleteBehavior:DeleteBehavior.NoAction);
    }
}