using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using simApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace simApi.Data;
[Table("Coupon", Schema = "dbo")]

public class Coupon:BaseModel
{
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }

   
}
public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{

    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);


        builder.Property(x => x.Code).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.ExpiryDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);




    }
}