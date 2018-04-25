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
    public class AccountGrain : Grain, IAccountGrain
    {
        private readonly IRepository _repository;

        public AccountGrain(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult> Create(CreateOperation<User> model)
        {
            try
            {
                if (_repository.Any<User>(x =>
                    x.Email == model.Entity.Email || x.IdentityUserId == model.Entity.IdentityUserId))
                    throw new Exception("User with the same properties already exists.");

                _repository.Add(model.Entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> Update(UpdateOperation<User> model)
        {
            try
            {
                var entityToUpdate = _repository.Find<User>(x => x.Id == model.Entity.Id);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.FirstName = model.Entity.FirstName;
                entityToUpdate.LastName = model.Entity.LastName;
                entityToUpdate.Email = model.Entity.Email;
                entityToUpdate.Roles = model.Entity.Roles;

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

        public async Task<IOperationResult> Delete(DeleteOperation<User> model)
        {
            try
            {
                var entityToMarkAsDeleted = _repository.Find<User>(user => user.Id == model.EntityId);

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
    }
}