using Core.Security.Entities;
using Core.Security.Hashing;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class UserOperationClaimConfiguration:IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(uoc => uoc.Id);

        builder.Property(uoc => uoc.Id).HasColumnName("Id").IsRequired();
        builder.Property(uoc => uoc.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(uoc => uoc.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();
        
        builder.HasQueryFilter(u=> !u.DeletedDate.HasValue);
        
        builder
            .HasOne(u=> u.User)
            .WithMany(u => u.UserOperationClaims)
            .HasForeignKey(u=>u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(u=> !u.DeletedDate.HasValue);
        
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(c => c.User)
            .WithMany(u => u.UserOperationClaims)
            .HasForeignKey(uoc => uoc.OperationClaimId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uoc => uoc.OperationClaim)
            .WithMany(o => o.UserOperationClaims)
            .HasForeignKey(o => o.OperationClaimId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(getData());
    }

    private HashSet<UserOperationClaim> getData()
    {
        HashSet<UserOperationClaim> operationClaims = new();
        UserOperationClaim userOperationClaim = new (1,userId:1,operationClaimId:3);
        UserOperationClaim userOperationClaim2 = new (2,userId:1,operationClaimId:1);
        
        operationClaims.Add(userOperationClaim);
        operationClaims.Add(userOperationClaim2);
        
        return operationClaims;
    }
}