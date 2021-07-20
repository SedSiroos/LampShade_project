using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CommentAgg;

namespace Shopmanagement.infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey("Id");
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Message).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Comment).HasForeignKey(x => x.ProductId);
        }
    }
}
