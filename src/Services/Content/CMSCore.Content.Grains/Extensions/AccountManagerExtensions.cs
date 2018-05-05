using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;

namespace CMSCore.Content.Grains.Extensions
{
    internal static class AccountManagerExtensions
    {
        public static User CreateModel(this CreateUserViewModel model)
        {
            return new User(model.IdentityUserId)
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        } 
    }
}