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
        private readonly ILogger<AccountGrain> _logger;

        public ContentGrain(ContentDbContext context, ILogger<AccountGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<AccountGrain>>();
            _context.Pages.Include(x => x.EntityHistory).Include(x => x.StaticContent).ThenInclude(x => x.EntityHistory)
                .Load();
            _context.Blogs.Include(x => x.EntityHistory).Include(x => x.BlogPosts).ThenInclude(x => x.EntityHistory)
                .Load();
        }

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

        public async Task<IEnumerable<Blog>> Blogs()
        {
            try
            {
                return await _context.Set<Blog>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<BlogPost>> BlogPosts()
        {
            try
            {
                return await _context.Set<BlogPost>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<BlogPost>> BlogPosts(string blogId)
        {
            try
            {
                return await _context.Set<BlogPost>().Where(x => x.BlogId == blogId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<BlogPost> BlogPostDetails(string blogPostId)
        {
            try
            {
                return await _context.Set<BlogPost>().FirstOrDefaultAsync(x => x.Id == blogPostId);
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
                    .Include(x => x.Blog)
                    .ThenInclude(x => x.BlogPosts)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<RemovedEntity>> RemovedEntities()
        {
            try
            {
                return await _context.Set<RemovedEntity>().ToListAsync();
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


        #region Create

        public async Task<IOperationResult> Create(CreateOperation<Page> model)
        {
            try
            {
                _context.Add(model.Entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Create(CreateOperation<BlogPost> model)
        {
            try
            {
                _context.Add(model.Entity);
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
                var entityToUpdate = await _context.FindAsync<Page>(model.EntityId);
                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;
                entityToUpdate.StaticContent = model.Entity?.StaticContent;

                entityToUpdate.EntityHistory.Add(new EntityHistory(model.EntityId, model.UserId, OperationType.Update));

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

        public async Task<IOperationResult> Update(UpdateOperation<Blog> model)
        {
            try
            {
                var entityToUpdate = await _context.FindAsync<Blog>(model.EntityId);
                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;
                entityToUpdate.EntityHistory.Add(new EntityHistory(model.Entity.Id, model.UserId,
                    OperationType.Update));

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

        public async Task<IOperationResult> Update(UpdateOperation<BlogPost> model)
        {
            try
            {
                var entityToUpdate = await _context.FindAsync<BlogPost>(model.EntityId);
                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;
                entityToUpdate.Content.Content = model.Entity.Content.Content;
                entityToUpdate.EntityHistory.Add(new EntityHistory(model.Entity.Id, model.UserId,
                    OperationType.Update));

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

        public async Task<IOperationResult> Update(UpdateOperation<StaticContent> model)
        {
            try
            {
                var entityToUpdate = await _context.FindAsync<StaticContent>(model.EntityId);
                if (entityToUpdate == null)
                    throw new Exception($"{model.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Content = model.Entity.Content;
                entityToUpdate.IsContentMarkdown = model.Entity.IsContentMarkdown;
                entityToUpdate.EntityHistory.Add(new EntityHistory(model.Entity.Id, model.UserId,
                    OperationType.Update));

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

        #endregion

        #region Delete

        public async Task<IOperationResult> Delete(DeleteOperation<Page> model)
        {
            try
            {
                var entityToMarkAsDeleted = await _context.FindAsync<Page>(model.EntityId);

                entityToMarkAsDeleted.IsRemoved = true;

                if (entityToMarkAsDeleted.PageContentType == PageContentType.Blog)
                {
                    entityToMarkAsDeleted.Blog.IsRemoved = true;
                    foreach (var entity in entityToMarkAsDeleted.Blog?.BlogPosts)
                    {
                        entity.IsRemoved = true;
                        entity.Content.IsRemoved = true;
                    }
                }
                else
                {
                    entityToMarkAsDeleted.StaticContent.IsRemoved = true;
                }

                _context.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _context.Add(removedEntity);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<Blog> model)
        {
            try
            {
                var entityToMarkAsDeleted = await _context.FindAsync<Blog>(model.EntityId);

                entityToMarkAsDeleted.IsRemoved = true;

                foreach (var entity in entityToMarkAsDeleted.BlogPosts)
                {
                    entity.IsRemoved = true;
                    entity.Content.IsRemoved = true;
                }

                _context.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _context.Add(removedEntity);

                await _context.SaveChangesAsync();

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<BlogPost> model)
        {
            try
            {
                return await _context.DeleteEntity<BlogPost>(model.EntityId, model.UserId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<StaticContent> model)
        {
            try
            {
                return await _context.DeleteEntity<StaticContent>(model.EntityId, model.UserId);
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