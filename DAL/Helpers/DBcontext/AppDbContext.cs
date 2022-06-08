
using WafferAPIs.DAL.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Auth;

namespace WafferAPIs.Dbcontext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Seller> Sellers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Seller>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.ContactPhoneNumber).IsUnique();
                entity.HasIndex(e => e.CustomerServicePhoneNumber).IsUnique();
            });

            base.OnModelCreating(builder);

        }
    }
}
