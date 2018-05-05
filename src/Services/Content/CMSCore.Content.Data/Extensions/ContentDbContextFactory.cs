using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMSCore.Content.Data
{
    public class ContentDbContextFactory : IDesignTimeDbContextFactory<ContentDbContext>
    {
        public ContentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseNpgsql(DatabaseConnectionConst.CMSCore);

            return new ContentDbContext(optionsBuilder.Options);
        }
    }
}