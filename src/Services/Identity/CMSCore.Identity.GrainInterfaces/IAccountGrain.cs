using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Identity.Models.ManageViewModels;
using CMSCore.Shared.Abstractions.Types.Results;
using Orleans;

namespace CMSCore.Identity.GrainInterfaces
{
    public interface IIdentityManagerGrain : IGrainWithGuidKey
    {
        Task<IOperationResult> ChangePassword(string userId, ChangePasswordViewModel model);
        Task<IdentityUserViewModel> GetIdentityUserViewModel(string userId);
        Task SendVerificationEmail(IdentityUserViewModel model);
        Task<IOperationResult> SetPassword(string userId, SetPasswordViewModel model);
        Task<IOperationResult> UpdateIdentityUser(string userId, IdentityUserViewModel model);

        Task<IOperationResult> CreateRole(string roleName);
        Task<IOperationResult> ManageUserRoles(string userId, IList<string> roles);

        Task<IOperationResult> CreateRegisterInvite(string toEmail);
        Task<IOperationResult> VerifyEmail(string userId, string code);
        Task<IOperationResult> AcceptInvitation(string userId, string code, string password);

        Task<IEnumerable<string>> GetIdentityRoles();
    }
}