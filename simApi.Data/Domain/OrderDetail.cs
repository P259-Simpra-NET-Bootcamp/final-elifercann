using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using simApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace simApi.Data;
[Table("OrderDetail", Schema = "dbo")]

public class OrderDetail:BaseModel
{
 
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{

   
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.ProductId).IsRequired(true);
        builder.Property(x => x.Quantity).IsRequired(true);
        builder.Property(x => x.Price).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);



    }
}