using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder)
        {
        }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<Brand> brands { get; set; }
        public virtual DbSet<Details> details { get; set; }
        public virtual DbSet<Image> images { get; set; }
        public virtual DbSet<Rating> ratings { get; set; }
        public virtual DbSet<ProductsCategories> productsCategories { get; set; }
        public virtual DbSet<ProductsOrders> orderProducts { get; set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<DeviceInfo> deviceInfo { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
