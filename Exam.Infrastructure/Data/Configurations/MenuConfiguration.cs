using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("menus");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).HasColumnName("id").IsRequired();
            builder.Property(m => m.RestaurantId).HasColumnName("restaurant_id").IsRequired();
            builder.Property(m => m.Name).HasColumnName("name").IsRequired();
            builder.Property(m => m.Description).HasColumnName("description");
            builder.Property(m => m.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(m => m.Category).HasColumnName("category");
            builder.Property(m => m.IsAvailable).HasColumnName("is_available").IsRequired();
            builder.Property(m => m.PreparationTime).HasColumnName("preparation_time").IsRequired();
            builder.Property(m => m.Weight).HasColumnName("weight").IsRequired();
            builder.Property(m => m.PhotoUrl).HasColumnName("photo_url");

            builder.HasOne<Restaurant>()
                   .WithMany()
                   .HasForeignKey(m => m.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}