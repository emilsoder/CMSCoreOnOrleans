using CMSCore.Content.Data;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces.Types;

namespace CMSCore.Content.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ContentDbContext _context;

        public RepositoryManager(ContentDbContext context)
        {
            _context = context;
            _context.LoadRelatedEntities();
        }

        public async Task<IOperationResult> UpdatePage(UpdatePageViewModel viewModel, string entityId,
            string currentUserId)
        {
            if (viewModel == null) throw new Exception("viewModel is null");
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = await _context.Pages.ToListAsync();

                if (set == null) throw new Exception("No entities found");

                var entityToUpdate = set.FirstOrDefault(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.Name = viewModel.Name;
                entityToUpdate.FeedEnabled = viewModel.FeedEnabled;
                entityToUpdate.StaticContent.Content = viewModel.Content;
                entityToUpdate.StaticContent.IsContentMarkdown = viewModel.IsContentMarkdown;

                entityToUpdate.EntityHistory.Add(new EntityHistory(entityId, currentUserId, OperationType.Update));

                _context.Update(entityToUpdate);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> UpdateFeed(UpdateFeedViewModel viewModel, string entityId,
            string currentUserId)
        {
            if (viewModel == null) throw new Exception("viewModel is null");
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = await _context.Feeds
                    .ToListAsync();

                if (set == null) throw new Exception("No entities found");

                var entityToUpdate = set.FirstOrDefault(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.Name = viewModel.Name;

                entityToUpdate.EntityHistory.Add(new EntityHistory(entityId, currentUserId, OperationType.Update));

                _context.Update(entityToUpdate);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> UpdateFeedItem(UpdateFeedItemViewModel viewModel, string entityId,
            string currentUserId)
        {
            if (viewModel == null) throw new Exception("viewModel is null");
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = await _context.FeedItems.ToListAsync();

                if (set == null) throw new Exception("No entities found");

                var entityToUpdate = set.FirstOrDefault(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.Title = viewModel.Title;
                entityToUpdate.StaticContent.Content = viewModel.Content;
                entityToUpdate.StaticContent.IsContentMarkdown = viewModel.IsContentMarkdown;
                entityToUpdate.CommentsEnabled = viewModel.CommentsEnabled;
                entityToUpdate.Description = viewModel.Description;
                entityToUpdate.Tags = entityToUpdate.Tags.AsTagCollection(viewModel.Tags);

                entityToUpdate.EntityHistory.Add(new EntityHistory(entityId, currentUserId, OperationType.Update));

                _context.Update(entityToUpdate);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> UpdateUser(UpdateUserViewModel viewModel, string entityId,
            string currentUserId)
        {
            if (viewModel == null) throw new Exception("viewModel is null");
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = await _context.Users.ToListAsync();

                if (set == null) throw new Exception("No entities found");

                var entityToUpdate = set.FirstOrDefault(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.FirstName = viewModel.FirstName;
                entityToUpdate.LastName = viewModel.LastName;
                entityToUpdate.Email = viewModel.Email;

                entityToUpdate.EntityHistory.Add(new EntityHistory(entityId, currentUserId, OperationType.Update));

                _context.Update(entityToUpdate);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        //------------------

        public async Task<IOperationResult> DeleteFeedAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Feeds.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.Delete));
                _context.Update(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> DeleteFeedItemAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.FeedItems.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.Delete));
                _context.Update(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> DeletePageAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Pages.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.IsDisabled = true;

                entity.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.Delete));
                _context.Update(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> DeleteUserAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Users.Include(x => x.EntityHistory).ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.IsDisabled = true;

                entity.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.Delete));
                _context.Update(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }


        // -----------------

        public async Task<IOperationResult> ConfirmDeleteFeedAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Feeds.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                _context.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.ConfirmDelete));

                if (entity.FeedItems != null && entity.FeedItems.Any())
                {
                    foreach (var feedItem in entity.FeedItems)
                    {
                        var res = await ConfirmDeleteFeedItemAsync(feedItem.Id, currentUserId, false);
                    }
                }

                _context.Remove(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> ConfirmDeleteFeedItemAsync(string entityId, string currentUserId, bool saveChanges = true)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.FeedItems.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                if (entity.StaticContent != null)
                {
                    _context.EntityHistory.Add(new EntityHistory(entity.StaticContent.Id, currentUserId,
                    OperationType.ConfirmDelete));
                    _context.Remove(entity.StaticContent);
                }

                var tagHistory = entity.Tags?.Select(x =>
                    new EntityHistory(x.Id, currentUserId, OperationType.ConfirmDelete));
                if (tagHistory != null)
                {
                    _context.AddRange(tagHistory);
                    _context.RemoveRange(entity.Tags);
                }
                _context.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.ConfirmDelete));
                _context.Remove(entity);

                if (!saveChanges)
                {
                    return OperationResult.Success;
                }

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> ConfirmDeletePageAsync(string entityId, string currentUserId)
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Pages.ToList();
                var entity = set.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                if (entity.Feed != null)
                    await ConfirmDeleteFeedAsync(entity.Feed.Id, currentUserId);

                _context.Remove(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }


        #region Generic

        public async Task<IOperationResult> DeleteAsync<TEntity>(string entityId, string currentUserId)
            where TEntity : EntityBase
        {
            if (string.IsNullOrEmpty(entityId)) throw new Exception("EntityId is null");
            try
            {
                var set = _context.Set<TEntity>()?.Include(x => x.EntityHistory);
                var entity =
                    await (set ?? throw new Exception("No entities found in set."))
                        .FirstOrDefaultAsync(x => x.Id == entityId);
                if (entity == null)
                    throw new Exception("Entity to perform delete operation could not be loaded.");

                entity.IsRemoved = true;
                entity.EntityHistory.Add(new EntityHistory(entity.Id, currentUserId, OperationType.Delete));
                _context.Update(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }


        public async Task<IOperationResult> CreateAsync<T>(T entity, string currentUserId)
            where T : EntityBase
        {
            if (entity == null) throw new Exception("Entity is null");
            try
            {
                entity.EntityHistory = new List<EntityHistory>()
                {
                    new EntityHistory(entity.Id,
                        currentUserId,
                        OperationType.Create)
                };

                _context.Add(entity);

                var result = await _context.SaveChangesAsync();

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : EntityBase
        {
            return await _context.Set<TEntity>().AnyAsync(expression);
        }

        #endregion
    }
}