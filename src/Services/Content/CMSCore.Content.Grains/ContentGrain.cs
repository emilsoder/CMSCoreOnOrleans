using System;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions;
using EFCore.Repository;
using Orleans;

namespace CMSCore.Content.Grains
{
    public class ContentGrain : Grain, IContentGrain
    {
        private readonly IRepository _repository;

        public ContentGrain(IRepository repository)
        {
            _repository = repository;
        }

        #region Create

        public async Task<IOperationResult> Create(CreateOperation<Page> model)
        {
            try
            {
                _repository.Add(model.Entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Create(CreateOperation<BlogPost> model)
        {
            try
            {
                _repository.Add(model.Entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        #endregion

        #region Update

        public async Task<IOperationResult> Update(UpdateOperation<Page> model)
        {
            try
            {
                var entityToUpdate = _repository.Find<Page>(x => x.Id == model.Entity.Id);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;

                _repository.Update(entityToUpdate);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToUpdate.Id, model.UserId, OperationType.Update);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<Blog> model)
        {
            try
            {
                var entityToUpdate = _repository.Find<Blog>(x => x.Id == model.Entity.Id);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;

                _repository.Update(entityToUpdate);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToUpdate.Id, model.UserId, OperationType.Update);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<BlogPost> model)
        {
            try
            {
                var entityToUpdate = _repository.Find<BlogPost>(x => x.Id == model.Entity.Id);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Description = model.Entity.Description;
                entityToUpdate.Title = model.Entity.Title;
                entityToUpdate.Content.Content = model.Entity.Content.Content;

                _repository.Update(entityToUpdate);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToUpdate.Id, model.UserId, OperationType.Update);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<StaticContent> model)
        {
            try
            {
                var entityToUpdate = _repository.Find<StaticContent>(x => x.Id == model.Entity.Id);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.Content = model.Entity.Content;
                entityToUpdate.IsContentMarkdown = model.Entity.IsContentMarkdown;

                _repository.Update(entityToUpdate);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToUpdate.Id, model.UserId, OperationType.Update);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        #endregion

        #region Delete

        public async Task<IOperationResult> Delete(DeleteOperation<Page> model)
        {
            try
            {
                var entityToMarkAsDeleted = _repository.Find<Page>(page => page.Id == model.EntityId);

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

                _repository.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _repository.Add(removedEntity);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToMarkAsDeleted.Id, model.UserId, OperationType.Delete);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<Blog> model)
        {
            try
            {
                var entityToMarkAsDeleted = _repository.Find<Blog>(page => page.Id == model.EntityId);

                entityToMarkAsDeleted.IsRemoved = true;

                foreach (var entity in entityToMarkAsDeleted?.BlogPosts)
                {
                    entity.IsRemoved = true;
                    entity.Content.IsRemoved = true;
                }

                _repository.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _repository.Add(removedEntity);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToMarkAsDeleted.Id, model.UserId, OperationType.Delete);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<BlogPost> model)
        {
            try
            {
                var entityToMarkAsDeleted = _repository.Find<BlogPost>(page => page.Id == model.EntityId);

                entityToMarkAsDeleted.IsRemoved = true;
                entityToMarkAsDeleted.Content.IsRemoved = true;

                _repository.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _repository.Add(removedEntity);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToMarkAsDeleted.Id, model.UserId, OperationType.Delete);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Delete(DeleteOperation<StaticContent> model)
        {
            try
            {
                var entityToMarkAsDeleted = _repository.Find<StaticContent>(page => page.Id == model.EntityId);

                entityToMarkAsDeleted.IsRemoved = true;

                _repository.Update(entityToMarkAsDeleted);

                var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, model.UserId);
                _repository.Add(removedEntity);

                await _repository.SaveChangesAsync();
                await _repository.AggregateEntityHistory(entityToMarkAsDeleted.Id, model.UserId, OperationType.Delete);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        #endregion

        #region Private helpers

        //private async Task AggregateEntityHistory(string entityId, string userId, OperationType operationType)
        //{
        //    var entityHistory = new EntityHistory(entityId, userId, operationType);
        //    _repository.Add(entityHistory);
        //    await _repository.SaveChangesAsync();
        //}

        #endregion
    }
}