using CMSCore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMSCore.Identity.Data
{
    public class CMSCoreIdentityDbContext : IdentityDbContext
    {
        public CMSCoreIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser<string>>()
                .ToTable("IdentityUsers")
                .Property(p => p.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("IdentityUsers")
                .Property(p => p.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("IdentityUserRoles");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("IdentityUserLogins");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("IdentityUserClaims");

            modelBuilder.Entity<IdentityRole<string>>()
                .ToTable("IdentityRoles");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("IdentityUserTokens");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("IdentityUserRoleClaims");
        }
    }
}