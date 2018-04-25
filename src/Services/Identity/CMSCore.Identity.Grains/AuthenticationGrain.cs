using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CMSCore.Identity.GrainInterfaces;
using CMSCore.Identity.Models;
using CMSCore.Identity.Models.AccountViewModels;
using CMSCore.Identity.Models.ManageViewModels;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Orleans;

namespace CMSCore.Identity.Grains
{
    public class AuthenticationGrain : Grain, IAuthenticationGrain
    {
        private readonly ILogger<AuthenticationGrain> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

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

                return await _userManager.CreateAsync(new ApplicationUser {Email = model.Email}, model.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
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
                    _logger.LogWarning(
                        $"Unauthorized sign in attempt by user '{user.Id}' with provided email '{user.Email}'.");
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
                    JwtToken = user.CreateJwtToken(userRoles)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return null;
            }
        }
    }

    public static class JwtTokenUtility
    {
        public static string CreateJwtToken(this ApplicationUser user, IList<string> roles)
        {
            var stringifiedRoles = roles.ArrayToCommaSeparatedString();
            return user.CreateJwtToken(stringifiedRoles);
        }

        public static string CreateJwtToken(this ApplicationUser user, string roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(IdentityConst.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.NormalizedUserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, roles)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}