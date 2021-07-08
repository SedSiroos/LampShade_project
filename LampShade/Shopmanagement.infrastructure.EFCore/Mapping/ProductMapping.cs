using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace Shopmanagement.infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(150).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(250);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(150).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
