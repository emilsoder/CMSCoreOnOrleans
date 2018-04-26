using System;
using CMSCore.Content.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMSCore.Content.Data
{
    public class ContentDbContext : DbContext
    {
        public ContentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FeedItem> FeedItems { get; set; }
         public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public DbSet<StaticContent> StaticContents { get; set; }

        public DbSet<EntityHistory> EntityHistory { get; set; }
        public DbSet<RemovedEntity> RemovedEntities { get; set; }
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Page>().HasKey(x => x.Id);

            modelBuilder.Entity<Feed>().HasKey(x => x.Id);
            modelBuilder.Entity<FeedItem>().HasKey(x => x.Id);
            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);

            modelBuilder.Entity<StaticContent>().HasKey(x => x.Id);
            modelBuilder.Entity<EntityHistory>().HasKey(x => x.Id);
            modelBuilder.Entity<RemovedEntity>().HasKey(x => x.Id);
             

            modelBuilder.Entity<User>()
                .HasIndex(x => x.IdentityUserId)
                .IsUnique();

            modelBuilder.Entity<FeedItem>()
                .HasOne(x => x.Feed)
                .WithMany(x => x.FeedItems)
                .HasForeignKey(x => x.FeedId);
        }
    }

    public class ContentDbContextOptions
    {
        public static DbContextOptions DefaultPostgresOptions =>
            new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore)
                .UseLazyLoadingProxies()
                .Options;

        public static Action<DbContextOptionsBuilder> DefaultPostgresOptionsBuilder
            => builder => new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore);
        //.UseLazyLoadingProxies();

        public static DbContextOptionsBuilder DefaultPostgresOptionsBuild
            => new DbContextOptionsBuilder()
                .UseNpgsql(DatabaseConnectionConst.CMSCore)
                .UseLazyLoadingProxies();
    }

    public class ContentDbContextFactory : IDesignTimeDbContextFactory<ContentDbContext>
    {
        public ContentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseNpgsql(DatabaseConnectionConst.CMSCore);

            return new ContentDbContext(optionsBuilder.Options);
        }
    }

    public class DatabaseConnectionConst
    {
        public const string CMSCore =
            "Host=localhost;Port=5432;Database=cmscoredb_dev2;User ID=postgres; Password=postgres";
    }
}