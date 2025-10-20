using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class CourierConfiguration : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.ToTable("couriers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id").IsRequired();
            builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(c => c.Status).HasColumnName("status").IsRequired();
            builder.Property(c => c.CurrentLocation).HasColumnName("current_location").IsRequired().HasMaxLength(200);
            builder.Property(c => c.Rating).HasColumnName("rating").IsRequired().HasColumnType("decimal(4,2)");
            builder.Property(c => c.TransportType).HasColumnName("transport_type").IsRequired();

            builder.HasIndex(c => c.UserId).IsUnique();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}