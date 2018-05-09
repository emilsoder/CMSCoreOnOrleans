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
                var entityToUpdate = await _context.FindActiveEntityAsync<Page>(entityId);

                if (entityToUpdate.StaticContent.IsContentMarkdown != viewModel.IsContentMarkdown ||
                    entityToUpdate.StaticContent.Content != viewModel.Content)
                {
                    entityToUpdate.StaticContent.Content = viewModel.Content;
                    entityToUpdate.StaticContent.IsContentMarkdown = viewModel.IsContentMarkdown;
                    var r1 = await _context.UpdateEntityAsync(entityToUpdate.StaticContent, currentUserId);
                    if (entityToUpdate.Name != viewModel.Name || entityToUpdate.FeedEnabled != viewModel.FeedEnabled)
                    {
                        entityToUpdate.Name = viewModel.Name;
                        entityToUpdate.FeedEnabled = viewModel.FeedEnabled;
                        var r2 = await _context.UpdateEntityAsync(entityToUpdate.StaticContent, currentUserId);
                        return r2 > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
                    }
                    return r1 > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
                }

                if (entityToUpdate.Name != viewModel.Name || entityToUpdate.FeedEnabled != viewModel.FeedEnabled)
                {
                    entityToUpdate.Name = viewModel.Name;
                    entityToUpdate.FeedEnabled = viewModel.FeedEnabled;
                    var r2 = await _context.UpdateEntityAsync(entityToUpdate.StaticContent, currentUserId);
                    return r2 > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
                }

                return OperationResult.Failed("No rows changed");
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
                var entityToUpdate = await _context.FindActiveEntityAsync<Feed>(entityId);


                entityToUpdate.Name = viewModel.Name;


                var result = await _context.UpdateEntityAsync(entityToUpdate, currentUserId);


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
            try
            {
                var entityToUpdate = await _context.FindActiveEntityAsync<FeedItem>(entityId);

                entityToUpdate.Title = viewModel.Title;
                entityToUpdate.StaticContent.Content = viewModel.Content;
                entityToUpdate.StaticContent.IsContentMarkdown = viewModel.IsContentMarkdown;
                entityToUpdate.CommentsEnabled = viewModel.CommentsEnabled;
                entityToUpdate.Description = viewModel.Description;
                entityToUpdate.Tags = entityToUpdate.Tags.AsTagCollection(viewModel.Tags);

                var result = await _context.UpdateEntityAsync(entityToUpdate, currentUserId);


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


                var entityToUpdate = set.FirstOrDefault(x => x.Id == entityId);

                if (entityToUpdate == null)
                    throw new Exception($"Entity to update was not found.");

                entityToUpdate.FirstName = viewModel.FirstName;
                entityToUpdate.LastName = viewModel.LastName;
                entityToUpdate.Email = viewModel.Email;


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
                var result = await _context.MarkAsDeletedAsync<Feed>(entityId, currentUserId);

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
                var result = await _context.MarkAsDeletedAsync<FeedItem>(entityId, currentUserId);

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
                var result = await _context.MarkAsDeletedAsync<FeedItem>(entityId, currentUserId);

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
                var result = await _context.MarkAsDeletedAsync<FeedItem>(entityId, currentUserId);
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
                var result = await _context.ConfirmDeleteAsync<Feed>(entityId);

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> ConfirmDeleteFeedItemAsync(string entityId, string currentUserId,
            bool saveChanges = true)
        {
            try
            {
                var result = await _context.ConfirmDeleteAsync<FeedItem>(entityId);

                return result > 0 ? OperationResult.Success : OperationResult.Failed("No rows changed");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message + ex.InnerException?.Message);
            }
        }

        public async Task<IOperationResult> ConfirmDeletePageAsync(string entityId, string currentUserId)
        {
            try
            {
                var result = await _context.ConfirmDeleteAsync<Page>(entityId);

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
            try
            {
                var result = await _context.MarkAsDeletedAsync<FeedItem>(entityId, currentUserId);

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
            try
            {
                var result = await _context.CreateEntityAsync(entity, currentUserId);

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