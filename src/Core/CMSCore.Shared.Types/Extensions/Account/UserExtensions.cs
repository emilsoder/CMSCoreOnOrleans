using System;
using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;
using CMSCore.Shared.Types.Content.Account;
using CMSCore.Shared.Types.Content.EntityHistory;

namespace CMSCore.Shared.Types.Extensions.Account
{
    public static class UserExtensions
    {
        public static UserViewModel ViewModel(this User user)
        {
            return new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Created = user.Created,
                Email = user.Email,
                Id = user.Id,
                IdentityUserId = user.IdentityUserId,
                IsDisabled = user.IsDisabled,
                IsRemoved = user.IsRemoved,
                Modified = user.Modified,
                EntityHistory = user.EntityHistory?.Select(x => new EntityHistoryViewModel
                {
                    Id = x.Id,
                    EntityId = x.EntityId,
                    UserId = x.UserId,
                    Date = x.Date,
                    OperationType = x.OperationType,
                    OperationTypeName = Enum.GetName(typeof(OperationType), x.OperationType)
                })?.ToList()
            };
        }

        public static IEnumerable<UserViewModel> ViewModel(this IEnumerable<User> users)
        {
            return users.Select(x => x.ViewModel());
        }

        public static User CreateModel(this CreateUserViewModel model)
        {
            return new User(model.IdentityUserId)
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }
        public static User UpdateModel(this User entityToUpdate, UserViewModel model)
        {
            entityToUpdate.FirstName = model.FirstName;
            entityToUpdate.LastName = model.LastName;
            entityToUpdate.Email = model.Email;

            return entityToUpdate;
        }
    }
}