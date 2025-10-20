using Exam.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infrastructure.Data
{
    public class ExamDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options)
            : base(options)
        { }

        public DbSet<Courier> Couriers => Set<Courier>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamDbContext).Assembly);
        }
    }
}
