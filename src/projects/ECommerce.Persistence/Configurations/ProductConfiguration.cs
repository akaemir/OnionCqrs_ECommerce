using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product").HasKey(p => p.Id);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.Price).HasColumnName("Price").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description");
        builder.Property(p => p.Stock).HasColumnName("Stock").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder
            .HasMany(x=>x.ProductTags)
            .WithOne(x=>x.Product)
            .HasForeignKey(x=>x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(x=>x.ProductImages)
            .WithOne(x=>x.Product)
            .HasForeignKey(x=>x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}