using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("id").IsRequired();
            builder.Property(o => o.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(o => o.RestaurantId).HasColumnName("restaurant_id").IsRequired();
            builder.Property(o => o.CourierId).HasColumnName("courier_id");
            builder.Property(o => o.OrderStatus).HasColumnName("order_status").IsRequired();
            builder.Property(o => o.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(o => o.DeliveredAt).HasColumnName("delivered_at");
            builder.Property(o => o.TotalAmount).HasColumnName("total_amount").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(o => o.DeliveryAddress).HasColumnName("delivery_address").IsRequired();
            builder.Property(o => o.PaymentMethod).HasColumnName("payment_method").IsRequired();
            builder.Property(o => o.PaymentStatus).HasColumnName("payment_status").IsRequired();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Restaurant>()
                   .WithMany()
                   .HasForeignKey(o => o.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}