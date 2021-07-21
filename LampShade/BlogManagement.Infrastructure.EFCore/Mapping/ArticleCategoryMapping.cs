using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Keywords).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.MetaDescription).IsRequired();
            builder.Property(x => x.CanonicalAddress);
        }
    }
}
