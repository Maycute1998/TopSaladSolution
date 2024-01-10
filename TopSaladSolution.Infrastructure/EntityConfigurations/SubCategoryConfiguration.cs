using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopSaladSolution.Common.Enums;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Infrastructure.EntityConfigurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");
            builder.HasMany(x => x.Products).WithOne(x => x.SubCategories).HasForeignKey(x => x.SubCategoryId).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Status).HasDefaultValue(ItemStatus.Active);
        }
    }
}
