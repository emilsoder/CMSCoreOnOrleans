using System;
using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Api.Models;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Extensions
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
    }
}