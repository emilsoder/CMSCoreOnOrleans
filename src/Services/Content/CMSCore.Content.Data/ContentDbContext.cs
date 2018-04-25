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

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<StaticContent> StaticContents { get; set; }

        public DbSet<EntityHistory> EntityHistory { get; set; }
        public DbSet<RemovedEntity> RemovedEntities { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured) 
        //        optionsBuilder.UseNpgsql(DatabaseConnectionConst.CMSCore)
        //            .UseLazyLoadingProxies();

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Page>().HasKey(x => x.Id);

            modelBuilder.Entity<Blog>().HasKey(x => x.Id);
            modelBuilder.Entity<BlogPost>().HasKey(x => x.Id);
            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
            modelBuilder.Entity<StaticContent>().HasKey(x => x.Id);
            modelBuilder.Entity<EntityHistory>().HasKey(x => x.Id);
            modelBuilder.Entity<RemovedEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<BlogPostTag>()
                .HasKey(keys => new
                {
                    keys.BlogPostId,
                    keys.TagId
                });

            modelBuilder.Entity<User>()
                .HasIndex(x => x.IdentityUserId)
                .IsUnique();

            modelBuilder.Entity<BlogPost>()
                .HasOne(x => x.Blog)
                .WithMany(x => x.BlogPosts)
                .HasForeignKey(x => x.BlogId);
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
            "Host=localhost;Port=5432;Database=cmscoredb_dev1;User ID=postgres; Password=postgres";
    }
}