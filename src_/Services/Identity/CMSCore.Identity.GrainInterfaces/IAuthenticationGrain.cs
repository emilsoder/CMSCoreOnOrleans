using System.Threading.Tasks;
using CMSCore.Identity.Models.AccountViewModels;
using CMSCore.Identity.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Orleans;

namespace CMSCore.Identity.GrainInterfaces
{
    public interface IAuthenticationGrain : IGrainWithGuidKey
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignedInViewModel> SignIn(string email, string password);
    }
}