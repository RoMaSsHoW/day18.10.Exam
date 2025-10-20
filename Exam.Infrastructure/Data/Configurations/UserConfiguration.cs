using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id").IsRequired();
            builder.Property(u => u.UserName).HasColumnName("username").HasMaxLength(100).IsRequired();
            builder.Property(u => u.NormalizedUserName).HasColumnName("normalized_username").HasMaxLength(100);
            builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            builder.Property(u => u.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(255);
            builder.Property(u => u.EmailConfirmed).HasColumnName("email_confirmed");
            builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
            builder.Property(u => u.SecurityStamp).HasColumnName("security_stamp");
            builder.Property(u => u.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            builder.Property(u => u.PhoneNumber).HasColumnName("phone_number").HasMaxLength(30);
            builder.Property(u => u.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            builder.Property(u => u.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            builder.Property(u => u.LockoutEnd).HasColumnName("lockout_end");
            builder.Property(u => u.LockoutEnabled).HasColumnName("lockout_enabled");
            builder.Property(u => u.AccessFailedCount).HasColumnName("access_failed_count");
            builder.Property(u => u.Address).HasColumnName("address");
            builder.Property(u => u.RegistrationDate).HasColumnName("registration_date").IsRequired();
            builder.Property(u => u.Role).HasColumnName("role");

            builder.HasIndex(u => u.NormalizedEmail)
                   .HasDatabaseName("ix_users_normalized_email");
        }
    }
}
