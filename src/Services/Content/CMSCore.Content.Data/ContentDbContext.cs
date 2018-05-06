using System.Collections.Generic;
using CMSCore.Content.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            modelBuilder.Entity<User>()
                .HasIndex(x => x.IdentityUserId)
                .IsUnique();

            modelBuilder.Entity<FeedItem>()
                .HasOne(x => x.Feed)
                .WithMany(x => x.FeedItems)
                .HasForeignKey(x => x.FeedId);
        }

        public void LoadRelatedEntities()
        {
            this.Pages
                .Include(x => x.StaticContent)
                .Include(x => x.Feed)
                .ThenInclude(x => x.FeedItems)
                .Include(x => x.EntityHistory)
                .Load();

            this.FeedItems
                .Include(x => x.StaticContent)
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                 .Include(x => x.Feed).Include(x => x.EntityHistory)
                .Load();

            this.Users.Load();
         
            this.EntityHistory.Load();
        }
    }
}