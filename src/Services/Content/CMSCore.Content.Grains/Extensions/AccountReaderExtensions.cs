using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;

namespace CMSCore.Content.Grains.Extensions
{
    internal static class AccountReaderExtensions
    {
        internal static UserViewModel GetViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Created = user.Created,
                Email = user.Email,
                IdentityUserId = user.IdentityUserId,
                IsDisabled = user.IsDisabled,
                IsRemoved = user.IsRemoved,
                Modified = user.Modified
            };
        }

        internal static List<UserViewModel> GetViewModels(IEnumerable<User> users)
        {
            return users.Select(GetViewModel).ToList();
        }
    }
}