using System;
using Microsoft.EntityFrameworkCore;

namespace CMSCore.Content.Data
{
    public class ContentDbContextOptions
    {
        public static DbContextOptions DefaultPostgresOptions =>
            new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore)
                //.UseLazyLoadingProxies()
                .Options;

        public static Action<DbContextOptionsBuilder> DefaultPostgresOptionsBuilder
            => builder => new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore);
        //.UseLazyLoadingProxies();

        public static DbContextOptionsBuilder DefaultPostgresOptionsBuild
            => new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore);

        //.UseLazyLoadingProxies();
    }
}