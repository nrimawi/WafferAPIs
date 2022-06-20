
using WafferAPIs.DAL.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Auth;
using WafferAPIs.DAL.Entities;

namespace WafferAPIs.Dbcontext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Item> Items { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Seller>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.ContactPhoneNumber).IsUnique();
                entity.HasIndex(e => e.CustomerServicePhoneNumber).IsUnique();
            });
            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();

            });
            builder.Entity<SubCategory>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();

            });
            base.OnModelCreating(builder);

        }
    }
}
