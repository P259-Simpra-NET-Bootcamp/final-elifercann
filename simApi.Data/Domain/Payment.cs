using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using simApi.Base;

namespace simApi.Data;

public class Payment:BaseModel
{
    public string UserId { get; set; }
    public virtual AppUser User { get; set; }
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal CouponAmount { get; set; }
    public decimal PointAmount { get; set; }
    public string CouponCode { get; set; }
    public DateTime? PaymentDate { get; set; }
}
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{

    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.UserId).IsRequired(true);

        builder.Property(x => x.CouponCode).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.OrderNumber).IsRequired(false).HasMaxLength(9);
        builder.Property(x => x.CouponAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.PointAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.TotalAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.PaymentDate).IsRequired(false);


    }
}