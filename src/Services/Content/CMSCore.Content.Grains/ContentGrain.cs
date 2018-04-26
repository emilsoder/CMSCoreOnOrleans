using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace CMSCore.Content.Grains
{
    public class ContentGrain : Grain, IContentGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<ContentGrain> _logger;

        public ContentGrain(ContentDbContext context, ILogger<ContentGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<ContentGrain>>();

            _context.Pages
                .Include(x => x.StaticContent)
                .Include(x => x.Feed)
                .ThenInclude(x => x.FeedItems)
                .Include(x => x.EntityHistory)
                .Load();
        }

        #region Read

        public async Task<IEnumerable<Page>> Pages()
        {
            try
            {
                return await _context.Set<Page>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<Page> PageById(string id)
        {
            try
            {
                return await _context.Set<Page>()
                    .Include(x => x.Feed)
                    .ThenInclude(x => x.FeedItems)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<Page> PageByName(string name)
        {
            try
            {
                return await _context.Set<Page>()
                    .Include(x => x.StaticContent)
                    .Include(x => x.Feed)
                    .ThenInclude(x => x.FeedItems)
                    .FirstOrDefaultAsync(x => x.NormalizedName == name);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Feed>> Feeds()
        {
            try
            {
                return await _context.Set<Feed>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<FeedItem>> FeedItems()
        {
            try
            {
                return await _context.Set<FeedItem>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<FeedItem>> FeedItems(string feedId)
        {
            try
            {
                return await _context.Set<FeedItem>().Where(x => x.FeedId == feedId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<FeedItem> FeedItemDetails(string feedItemId)
        {
            try
            {
                return await _context.Set<FeedItem>().FirstOrDefaultAsync(x => x.Id == feedItemId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }


        public async Task<IEnumerable<EntityHistory>> EntityHistory(string entityId)
        {
            try
            {
                return await _context.Set<EntityHistory>().Where(x => x.EntityId == entityId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<EntityHistory>> EntityHistory()
        {
            try
            {
                return await _context.Set<EntityHistory>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        #endregion

        #region Create

        public async Task<IOperationResult> Create(CreateOperation<Page> model)
        {
            return await CreateEntity(model.Entity, model.UserId);
        }

        public async Task<IOperationResult> Create(CreateOperation<FeedItem> model)
        {
            return await CreateEntity(model.Entity, model.UserId);
        }

        private async Task<IOperationResult> CreateEntity<T>(T entity, string userId) where T : EntityBase
        {
            try
            {
                entity.EntityHistory.Add(new EntityHistory(entity.Id, userId,
                    OperationType.Create));

                _context.Add(entity);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        #endregion

        #region Update

        public async Task<IOperationResult> Update(UpdateOperation<Page> model)
        {
            try
            {
                var set = _context.Set<Page>()?
                    .Include(x => x.EntityHistory)
                    .Include(x => x.StaticContent);
                var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
                    .FirstOrDefaultAsync(x => x.Id == model.EntityId);


                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Name = model.Entity.Name;
                if (entityToUpdate.StaticContent?.Content == null)
                {
                    entityToUpdate.StaticContent = new StaticContent();
                }

                entityToUpdate.StaticContent.Content = model.Entity.StaticContent.Content;
                entityToUpdate.StaticContent.IsContentMarkdown = model.Entity.StaticContent.IsContentMarkdown;

                return await UpdateEntity(entityToUpdate, model.UserId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<Feed> model)
        {
            try
            {
                var set = _context.Set<Feed>()?
                    .Include(x => x.EntityHistory);

                var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
                    .FirstOrDefaultAsync(x => x.Id == model.EntityId);

                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Name = model.Entity.Name;
                return await UpdateEntity(entityToUpdate, model.UserId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<FeedItem> model)
        {
            try
            {
                var set = _context.Set<FeedItem>()?
                    .Include(x => x.EntityHistory);

                var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
                    .FirstOrDefaultAsync(x => x.Id == model.EntityId);

                if (entityToUpdate == null)
                    throw new Exception("Entity to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;
                entityToUpdate.StaticContent.Content = model.Entity.StaticContent.Content;
                entityToUpdate.StaticContent.IsContentMarkdown = model.Entity.StaticContent.IsContentMarkdown;
                entityToUpdate.Tags = model.Entity.Tags;
                entityToUpdate.CommentsEnabled = model.Entity.CommentsEnabled;

                return await UpdateEntity(entityToUpdate, model.UserId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        private async Task<IOperationResult> UpdateEntity<T>(T entity, string userId) where T : EntityBase
        {
            entity.EntityHistory.Add(new EntityHistory(entity.Id, userId,
                OperationType.Update));

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }
        #endregion

        #region Delete

        public async Task<IOperationResult> Delete(DeleteOperation<Page> operation)
        {
            return await DeleteEntity(operation);
        }

        public async Task<IOperationResult> Delete(DeleteOperation<Feed> operation)
        {
            return await DeleteEntity(operation);
        }

        public async Task<IOperationResult> Delete(DeleteOperation<FeedItem> operation)
        {
            return await DeleteEntity(operation);
        }

        public async Task<IOperationResult> DeleteEntity<T>(DeleteOperation<T> operation) where T : EntityBase
        {
            try
            {
                var set = _context.Set<T>()?.Include(x => x.EntityHistory);
                var entity =
                    await (set ?? throw new Exception("No entities found in set."))
                        .FirstOrDefaultAsync(x => x.Id == operation.EntityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.EntityHistory.Add(new EntityHistory(entity.Id, operation.UserId, OperationType.Delete));
                _context.Update(entity);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }
        #endregion

    }
}