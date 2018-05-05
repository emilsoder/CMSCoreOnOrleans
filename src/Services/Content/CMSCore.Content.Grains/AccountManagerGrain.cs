using System;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Grains.Extensions;
using CMSCore.Content.Models;
using CMSCore.Content.Services;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace CMSCore.Content.Grains
{
    public class AccountManagerGrain : Grain, IAccountManagerGrain
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<AccountManagerGrain> _logger;

        public AccountManagerGrain(IRepositoryManager repository, ILogger<AccountManagerGrain> logger = null)
        {
            _repository = repository;
            _logger = logger ?? ServiceProvider?.GetService<ILogger<AccountManagerGrain>>();
        }

        //public string UserId => this?.GetPrimaryKeyString();
        public string _userId;

        public string UserId
        {
            get => _userId ?? this?.GetPrimaryKeyString();
            set => _userId = value;
        }

        public async Task<IOperationResult> Create(CreateUserViewModel model)
        {
            try
            {
                if (await UserExistsAsync(model.Email, model.IdentityUserId))
                    throw new Exception("User with the same properties already exists.");

                var userToAdd = model.CreateModel();
                return await _repository.CreateAsync(userToAdd, UserId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        private async Task<bool> UserExistsAsync(string email, string identityUserId)
        {
            return await _repository.AnyAsync<User>(x =>
                x.Email == email
                || x.IdentityUserId == identityUserId);
        }

        public async Task<IOperationResult> Update(UpdateUserViewModel model, string entityId)
        {
            return await _repository.UpdateUser(model, entityId, UserId);
        }

        public async Task<IOperationResult> Delete(DeleteUserViewModel model)
        {
            return await _repository.DeleteAsync<User>(model.Id, UserId);
        }
    }
}