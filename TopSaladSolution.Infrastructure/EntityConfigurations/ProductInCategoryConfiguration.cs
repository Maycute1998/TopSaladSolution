using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(x => new { x.CategoryId, x.ProductId});

            builder.ToTable("ProductInCategory");

            builder.HasOne(p => p.Product)
                .WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(p => p.ProductId);

            builder.HasOne(c => c.Category)
             .WithMany(pc => pc.ProductInCategories)
             .HasForeignKey(p => p.CategoryId);
        }
    }
}
