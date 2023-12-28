using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Infrastructure.Enums;

namespace TopSaladSolution.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);

            builder.HasMany(x => x.SubCategories)
                .WithOne(x => x.Categories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();
        }
    }
}
