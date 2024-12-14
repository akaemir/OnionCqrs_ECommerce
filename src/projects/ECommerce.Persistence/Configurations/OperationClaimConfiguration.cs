using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class OperationClaimConfiguration :IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(c => c.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(oc => oc.UserOperationClaims)
            .WithOne(uop => uop.OperationClaim)
            .HasForeignKey(uop => uop.OperationClaimId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(getData());
    }

    private HashSet<OperationClaim> getData()
    {
        HashSet<OperationClaim> operationClaims = new()
        {
            new OperationClaim { Id = 1, Name = "Admin" },
            new OperationClaim { Id = 2, Name = "User" },
            new OperationClaim { Id = 3, Name = "MVP" },
        };
        return operationClaims;
    }
}