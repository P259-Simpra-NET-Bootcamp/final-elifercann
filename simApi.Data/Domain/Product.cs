﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace simApi.Data;

[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public int CategoryId { get; set; }
    public virtual Category Category  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Property { get; set; }
    public bool IsActive { get; set; }
    public decimal WinEarnPoints { get; set; }
    public decimal Price { get; set; }
    public int MaxTotalScore { get; set; }


}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.CategoryId).IsRequired(true);
       

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Property).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.WinEarnPoints).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.Price).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.MaxTotalScore).IsRequired(true).HasDefaultValue(0);

        

      


    }

}
