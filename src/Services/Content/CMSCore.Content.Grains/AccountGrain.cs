using System;
using System.Collections.Generic;
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
    public class AccountGrain : Grain, IAccountGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<AccountGrain> _logger;

        public AccountGrain(ContentDbContext context, ILogger<AccountGrain> logger = null)
        {
            _context = context;

            _logger = logger ?? ServiceProvider.GetService<ILogger<AccountGrain>>();
        }

        public async Task<IOperationResult> Create(CreateOperation<User> model)
        {
            try
            {
                if (await _context.Set<User>().AnyAsync(x =>
                    x.Email == model.Entity.Email || x.IdentityUserId == model.Entity.IdentityUserId))
                    throw new Exception("User with the same properties already exists.");

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

        public async Task<IOperationResult> Update(UpdateOperation<User> model)
        {
            try
            {
                var entityToUpdate = await _context.FindAsync<User>(model.EntityId);
                if (entityToUpdate == null)
                    throw new Exception($"{model?.Entity?.GetType()?.Name ?? "Entity"} to update was not found.");

                entityToUpdate.FirstName = model.Entity.FirstName;
                entityToUpdate.LastName = model.Entity.LastName;
                entityToUpdate.Email = model.Entity.Email;

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

        public async Task<IOperationResult> Delete(DeleteOperation<User> model)
        {
            try
            {
                return await DeleteEntity(model);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<User> Find(string userId)
        {
            try
            {
                return await _context.Set<User>().Include(x => x.EntityHistory)
                    .FirstOrDefaultAsync(x => x.Id == userId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<User>> ToList()
        {
            try
            {
                return await _context.Set<User>().Include(x => x.EntityHistory).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return new List<User>();
            }
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
    }
}