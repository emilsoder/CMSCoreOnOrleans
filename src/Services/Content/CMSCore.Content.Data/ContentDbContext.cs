using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Page>().HasKey(x => x.Id);

            modelBuilder.Entity<Feed>().HasKey(x => x.Id);
            modelBuilder.Entity<FeedItem>().HasKey(x => x.Id);
            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);

            //modelBuilder.Entity<StaticContent>().HasKey(x => x.Id);

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
        }

        public async Task <IEnumerable<TEntity>> GetEntityVersions<TEntity>(string entityId) where TEntity : EntityBase
        {
            try
            {
                var result = base.Set<TEntity>()?.Where(x => x.EntityId == entityId).OrderBy(x => x.Version);
                if (result != null) return result;
                
                var firstResult = await FindActiveEntityAsync<TEntity>(entityId);
                var result2 = Set<TEntity>().Where(x => x.EntityId == firstResult.EntityId);
                return result2;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<TEntity> FindActiveEntityAsync<TEntity>(string entityId) where TEntity : EntityBase
        {
            try
            {
                return Set<TEntity>()
                    ?.FirstOrDefaultAsync(x => x.EntityId == entityId || x.Id == entityId && x.IsActiveVersion == true);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> UpdateEntityAsync<TEntity>(TEntity entity, string currentUserId) where TEntity : EntityBase
        {
            try
            {
                var oldVersion = await Set<TEntity>()
                    .FirstOrDefaultAsync(x => x.EntityId == entity.EntityId && x.IsActiveVersion == true);
                if (oldVersion == null) return -1;
                oldVersion.IsActiveVersion = false;
                Update(oldVersion);

                entity.Version = oldVersion.Version + 1;
                entity.Id = Guid.NewGuid().ToString();
                entity.UserId = currentUserId;
                Add(entity);
                return await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> CreateEntityAsync<TEntity>(TEntity entity, string currentUserId) where TEntity : EntityBase
        {
            try
            { 
                entity.UserId = currentUserId;
                Add(entity);
                return await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> MarkAsDeletedAsync<TEntity>(string entityId, string currentUserId) where TEntity : EntityBase
        {
            try
            {
                var entities = await GetEntityVersions<TEntity>(entityId);
                var enumerable = entities.ToList();

                enumerable.ForEach(x =>
                {
                    x.UserId = currentUserId;
                    x.MarkedToDelete = true;
                    x.IsActiveVersion = false;
                });

                UpdateRange(enumerable);

                return await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> MarkEntityAsHiddenAsync<TEntity>(string entityId, string currentUserId) where TEntity : EntityBase
        {
            try
            {
                var entity = await Set<TEntity>()
                    .FirstOrDefaultAsync(x => x.EntityId == entityId && x.IsActiveVersion == true);
                if (entity == null) return -1;

                entity.UserId = currentUserId;
                entity.Hidden = true;
                Update(entity);

                return await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> ConfirmDeleteAsync<TEntity>(string entityId) where TEntity : EntityBase
        {
            try
            {
                var entitiesToDelete = Set<TEntity>().Where(x => x.EntityId == entityId);
                RemoveRange(entitiesToDelete);
                return await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEnumerable<TEntity> GetHiddenEntities<TEntity>() where TEntity : EntityBase
        {
            try
            {
                return Set<TEntity>()?.Where(x => x.Hidden == true);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<TEntity> GetActiveEntities<TEntity>(bool includeHidden = false) where TEntity : EntityBase
        {
            try
            {
                return includeHidden
                    ? Set<TEntity>()?.Where(x => x.IsActiveVersion == true)
                    : Set<TEntity>()?.Where(x => x.IsActiveVersion == true && x.Hidden == false);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}