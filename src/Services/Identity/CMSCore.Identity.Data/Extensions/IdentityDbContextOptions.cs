using System;
using Microsoft.EntityFrameworkCore;

namespace CMSCore.Identity.Data.Extensions
{
    public class IdentityDbContextOptions
    {
        public static DbContextOptions DefaultPostgresOptions =>
            new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.Identity)
                .Options;

        public static Action<DbContextOptionsBuilder> DefaultPostgresOptionsBuilder
            => builder => new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.Identity);
    }
}