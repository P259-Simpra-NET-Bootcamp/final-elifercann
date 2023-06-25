using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace simApi.Data;
[Table("Order", Schema = "dbo")]

public class Order : BaseModel
{
    public string UserId { get; set; }
    public virtual AppUser User { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }
    public decimal BasketAmount { get; set; }
    public decimal CouponAmount { get; set; }
    public decimal PointAmount { get; set; }
    public string CouponCode { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.UserId).IsRequired(true);

        builder.Property(x => x.CouponCode).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.OrderNumber).IsRequired(false).HasMaxLength(9);
        builder.Property(x => x.BasketAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.CouponAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.PointAmount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);

        builder.Property(x => x.IsActive).IsRequired(true);

        
      

    }
}