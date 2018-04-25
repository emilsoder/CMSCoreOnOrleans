using System;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Identity.Extensions;
using CMSCore.Identity.GrainInterfaces;
using CMSCore.Identity.Models;
using CMSCore.Identity.Models.AccountViewModels;
using CMSCore.Identity.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Orleans;

namespace CMSCore.Identity.Grains
{
    public class AuthenticationGrain : Grain, IAuthenticationGrain
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthenticationGrain> _logger;

        public AuthenticationGrain(
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthenticationGrain> logger,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "Error",
                        Description = "Passwords do not match."
                    });

                var result = await _userManager.CreateAsync(new ApplicationUser {Email = model.Email}, model.Password);
                return result;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Error",
                    Description = ex.Message
                });
            }
        }

        public async Task<SignedInViewModel> SignIn(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    throw new Exception("Wrong password or email.");

                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (!result.Succeeded || result.IsLockedOut || result.IsNotAllowed)
                {
                    return null;
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                return new SignedInViewModel
                {
                    Email = user.Email,
                    IdentityUserId = user.Id,
                    UserName = user.NormalizedUserName,
                    Roles = userRoles?.Select(role => new IdentityRoleViewModel(role)).ToArray(),
                    Message = "Success",
                    JwtToken = JwtTokenUtility.CreateJwtToken(user.NormalizedUserName, user.Email, userRoles?.ToArray())
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return null;
            }
        }
    }
}