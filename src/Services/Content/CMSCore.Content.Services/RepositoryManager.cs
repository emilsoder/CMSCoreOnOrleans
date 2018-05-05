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
            try
            {
                var set = await _context.Pages
                    .Include(x => x.StaticContent)
                    .Include(x => x.EntityHistory)
                    .ToListAsync();

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

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> UpdateFeed(UpdateFeedViewModel viewModel, string entityId,
            string currentUserId)
        {
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

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> UpdateFeedItem(UpdateFeedItemViewModel viewModel, string entityId,
            string currentUserId)
        {
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

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> UpdateUser(UpdateUserViewModel viewModel, string entityId,
            string currentUserId)
        {
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

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        #region Generic

        public async Task<IOperationResult> DeleteAsync<TEntity>(string entityId, string currentUserId)
            where TEntity : EntityBase
        {
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

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }


        public async Task<IOperationResult> CreateAsync<T>(T entity, string currentUserId)
            where T : EntityBase
        {
            try
            {
                entity.EntityHistory = new List<EntityHistory>()
                {
                    new EntityHistory(entity.Id,
                        currentUserId,
                        OperationType.Create)
                };

                _context.Add(entity);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
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