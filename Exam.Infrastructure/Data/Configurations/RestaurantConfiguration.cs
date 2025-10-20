using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("restaurants");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id").IsRequired();
            builder.Property(r => r.Name).HasColumnName("name").IsRequired();
            builder.Property(r => r.Address).HasColumnName("address").IsRequired();
            builder.Property(r => r.Rating).HasColumnName("rating").IsRequired().HasColumnType("decimal(4,2)");
            builder.Property(r => r.WorkingHours).HasColumnName("working_hours").IsRequired();
            builder.Property(r => r.Description).HasColumnName("description");
            builder.Property(r => r.ContactPhone).HasColumnName("contact_phone").IsRequired();
            builder.Property(r => r.IsActive).HasColumnName("is_active").IsRequired();
            builder.Property(r => r.MinOrderAmount).HasColumnName("min_order_amount").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(r => r.DeliveryPrice).HasColumnName("delivery_price").IsRequired().HasColumnType("decimal(10,2)");
        }
    }
}