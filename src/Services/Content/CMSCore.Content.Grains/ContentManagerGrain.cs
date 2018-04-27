using System;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Abstractions.Types.Results;
using CMSCore.Shared.Types.Content.Feed;
using CMSCore.Shared.Types.Extensions.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Orleans;

namespace CMSCore.Content.Grains
{
    public class ContentManagerGrain : Grain, IContentManagerGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<ContentManagerGrain> _logger;

        public ContentManagerGrain(ContentDbContext context, ILogger<ContentManagerGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<ContentManagerGrain>>();

            _context.Pages
                .Include(x => x.StaticContent)
                .Include(x => x.Feed)
                .ThenInclude(x => x.FeedItems)
                .Include(x => x.EntityHistory)
                .Load();
        }

        private string UserId => this.GetPrimaryKeyString();

        private DbSet<T> GetSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        private async Task<IOperationResult> CreateEntity<T>(T entity) where T : EntityBase
        {
            try
            {
                entity.EntityHistory.Add(new EntityHistory(entity.Id, UserId,
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

        public async Task<IOperationResult> Create(CreatePageViewModel model)
        {
            var entity = model.CreateModel();
            return await CreateEntity(entity);
        }

        public async Task<IOperationResult> Create(CreateFeedItemViewModel model)
        {
            var entity = model.CreateModel();
            return await CreateEntity(entity);
        }

        // -----------

        public async Task<IOperationResult> Update(UpdatePageViewModel model, string entityId)
        {
            return await UpdateEntity<Page>(model, entityId);

            //try
            //{
            //    var set = _context.Set<Page>()?
            //        .Include(x => x.EntityHistory)
            //        .Include(x => x.StaticContent);
            //    var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
            //        .FirstOrDefaultAsync(x => x.Id == entityId);


            //    if (entityToUpdate == null)
            //        throw new Exception($"Entity to update was not found.");

            //    return await UpdateEntity(entityToUpdate.UpdateModel(model));
            //}
            //catch (Exception ex)
            //{
            //    _logger?.LogError(ex);
            //    return OperationResult.Failed(ex.Message);
            //}
        }

        public async Task<IOperationResult> Update(UpdateFeedViewModel model, string entityId)
        {
            return await UpdateEntity<Feed>(model, entityId);
            //try
            //{
            //    var set = _context.Set<Feed>()?
            //        .Include(x => x.EntityHistory);

            //    var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
            //        .FirstOrDefaultAsync(x => x.Id == entityId);

            //    if (entityToUpdate == null)
            //        throw new Exception($"Entity to update was not found.");


            //    return await UpdateEntity(entityToUpdate.UpdateModel(model));
            //}
            //catch (Exception ex)
            //{
            //    _logger?.LogError(ex);
            //    return OperationResult.Failed(ex.Message);
            //}
        }

        public async Task<IOperationResult> Update(UpdateFeedItemViewModel model, string entityId)
        {
            return await UpdateEntity<FeedItem>(model, entityId);
            //try
            //{
            //    var set = _context.Set<FeedItem>()?
            //        .Include(x => x.EntityHistory);

            //    var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
            //        .FirstOrDefaultAsync(x => x.Id == entityId);

            //    if (entityToUpdate == null)
            //        throw new Exception($"Entity to update was not found.");

            //    return await UpdateEntity(entityToUpdate.UpdateModel(model));
            //}
            //catch (Exception ex)
            //{
            //    _logger?.LogError(ex);
            //    return OperationResult.Failed(ex.Message);
            //}
        }


        private async Task<IOperationResult> UpdateEntity<T>(T entity) where T : EntityBase
        {
            entity.EntityHistory.Add(new EntityHistory(entity.Id, UserId,
                OperationType.Update));

            _context.Update(entity);

            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }

        private async Task<IOperationResult> UpdateEntity<T>(object viewModel, string entityId) where T : EntityBase
        {
            try
            {
                var set = _context.Set<T>()?
                    .Include(x => x.EntityHistory);

                var entityToUpdate = await (set ?? throw new Exception("No entities found in set."))
                    .FirstOrDefaultAsync(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.UpdateModel(viewModel);
                entityToUpdate.EntityHistory.Add(new EntityHistory(entityId, UserId, OperationType.Update));

                _context.Update(entityToUpdate);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }


        // -----------

        public async Task<IOperationResult> Delete(DeletePageViewModel model)
        {
            return null;
        }

        public async Task<IOperationResult> Delete(DeleteFeedViewModel model)
        {
            return null;
        }

        public async Task<IOperationResult> Delete(DeleteFeedItemViewModel model)
        {
            return null;
        }
    }

    public static class ModelExtensions
    {
        public static TEntity UpdateModel<TEntity, TViewModel>(this TEntity entity, TViewModel model)
            where TEntity : EntityBase
            where TViewModel : class
        {
            switch (model)
            {
                case UpdatePageViewModel m:
                {
                    if (entity is Page page)
                        return page.UpdateModel(m) as TEntity;
                    break;
                }
                case UpdateFeedViewModel m:
                {
                    if (entity is Feed feed)
                        return feed.UpdateModel(m) as TEntity;
                    break;
                }
                case UpdateFeedItemViewModel m:
                {
                    if (entity is FeedItem feedItem)
                        return feedItem.UpdateModel(m) as TEntity;
                    break;
                }
            }

            return entity;
        }

        public static Page UpdateModel(this Page entityToUpdate, UpdatePageViewModel model)
        {
            entityToUpdate.Name = model.Name;
            entityToUpdate.FeedEnabled = model.FeedEnabled;
            entityToUpdate.StaticContent.Content = model.Content;
            entityToUpdate.StaticContent.IsContentMarkdown = model.IsContentMarkdown;
            return entityToUpdate;
        }

        public static Feed UpdateModel(this Feed entityToUpdate, UpdateFeedViewModel model)
        {
            entityToUpdate.Name = model.Name;
            return entityToUpdate;
        }

        public static FeedItem UpdateModel(this FeedItem entityToUpdate, UpdateFeedItemViewModel model)
        {
            entityToUpdate.Title = model.Title;
            entityToUpdate.StaticContent.Content = model.Content;
            entityToUpdate.StaticContent.IsContentMarkdown = model.IsContentMarkdown;
            entityToUpdate.CommentsEnabled = model.CommentsEnabled;
            entityToUpdate.Description = model.Description;
            entityToUpdate.Tags = entityToUpdate.Tags.AsTagCollection(model.Tags);
            return entityToUpdate;
        }
    }
}