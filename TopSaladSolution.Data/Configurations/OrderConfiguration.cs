using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopSaladSolution.Data.Entities;

namespace TopSaladSolution.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.RecipientName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);
            builder.Property(x => x.RecipientAddress)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.RecipientEmail)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.RecipientPhoneNumber)
                .IsRequired()
                .HasMaxLength(12);

            //Has one user
            builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        }
    }
}
