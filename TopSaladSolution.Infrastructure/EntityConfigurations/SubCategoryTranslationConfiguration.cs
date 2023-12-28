using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Data.Configurations
{
    public class SubCategoryTranslationConfiguration : IEntityTypeConfiguration<SubCategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<SubCategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SeoDescription).HasMaxLength(500);

            builder.Property(x => x.SeoTitle).HasMaxLength(200);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.SubCategoryTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.SubCategory).WithMany(x => x.SubCategoryTranslations).HasForeignKey(x => x.SubCategoryId);
        }
    }
}
