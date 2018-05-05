using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Shared.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using static CMSCore.Content.Grains.Extensions.AccountReaderExtensions;

namespace CMSCore.Content.Grains
{
    public class AccountReaderGrain : Grain, IAccountReaderGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<AccountReaderGrain> _logger;

        public AccountReaderGrain(ContentDbContext context, ILogger<AccountReaderGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<AccountReaderGrain>>();
        }

        private string ProvidedPrimaryKey => (this).GetPrimaryKeyString();

        public Task<UserViewModel> UserById()
        {
            try
            {
                var user = _context.Users.Find(ProvidedPrimaryKey);
                var vm = GetViewModel(user);

                return Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public Task<List<UserViewModel>> UsersToList()
        {
            try
            {
                var users = _context.Users;
                var vm = GetViewModels(users);

                return Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }
    }
}