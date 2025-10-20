using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("order_details");
            builder.HasKey(od => od.Id);

            builder.Property(od => od.Id).HasColumnName("id").IsRequired();
            builder.Property(od => od.OrderId).HasColumnName("order_id").IsRequired();
            builder.Property(od => od.MenuItemId).HasColumnName("menu_item_id").IsRequired();
            builder.Property(od => od.Quantity).HasColumnName("quantity").IsRequired();
            builder.Property(od => od.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(od => od.SpecialInstructions).HasColumnName("special_instructions");

            builder.HasOne<Order>()
                   .WithMany()
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Menu>()
                   .WithMany()
                   .HasForeignKey(od => od.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}