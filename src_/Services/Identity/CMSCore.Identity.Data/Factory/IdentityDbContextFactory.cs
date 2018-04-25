using CMSCore.Identity.Data.Extensions;
using Microsoft.EntityFrameworkCore.Design;

namespace CMSCore.Identity.Data.Factory
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<CMSCoreIdentityDbContext>
    {
        public CMSCoreIdentityDbContext CreateDbContext(string[] args)
        { 
            return new CMSCoreIdentityDbContext(IdentityDbContextOptions.DefaultPostgresOptions);
        }
    }
}