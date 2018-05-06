using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Identity.GrainInterfaces;
using CMSCore.Identity.Grains.Extensions;
using CMSCore.Identity.Models;
using CMSCore.Identity.Models.ManageViewModels;
using CMSCore.Identity.Services;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orleans;

namespace CMSCore.Identity.Grains
{
    public class IdentityManagerGrain : Grain, IIdentityManagerGrain
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<IdentityManagerGrain> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityManagerGrain(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<IdentityManagerGrain> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<IOperationResult> ChangePassword(string userId, ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new Exception("User not found");

                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                _logger.LogInformation($"User {user.Id} changed password.");

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IdentityUserViewModel> GetIdentityUserViewModel(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new Exception($"Unable to load user");

                return new IdentityUserViewModel
                {
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsEmailConfirmed = user.EmailConfirmed
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new IdentityUserViewModel {StatusMessage = ex.Message};
            }
        }

        public async Task SendVerificationEmail(IdentityUserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) throw new Exception($"Unable to load user");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = UrlHelperExtensions.EmailConfirmationLink(user.Id, code);

                var email = user.Email;
                await _emailSender.SendEmailAsync(email, "Confirm email", callbackUrl);

                _logger.LogInformation($"Verification email sent to {user.Id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public async Task<IOperationResult> SetPassword(string userId, SetPasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new Exception($"Unable to load user");

                if (await _userManager.HasPasswordAsync(user)) throw new Exception("User already has password.");

                await _userManager.AddPasswordAsync(user, model.NewPassword);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);

                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> UpdateIdentityUser(string userId, IdentityUserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    throw new Exception($"Unable to load user");

                if (model.Email != user.Email) await _userManager.SetEmailAsync(user, model.Email);

                if (model.PhoneNumber != user.PhoneNumber)
                    await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);

                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> CreateRole(string roleName)
        {
            try
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (roleExists) return OperationResult.Failed("Role already exists.");

                var addresult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                return addresult.Succeeded
                    ? OperationResult.Success
                    : OperationResult.Failed(addresult.Errors?.Select(x => x?.Description).ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> ManageUserRoles(string userId, IList<string> roles)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return OperationResult.Failed("User could not be loaded.");

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null)
                {
                    //var rolesToKeep = roles?.Where(role => userRoles.Contains(role))?.ToList();

                    var rolesToRemove = userRoles.Where(userRole => roles?.Contains(userRole) == false).ToList();
                    if (rolesToRemove.Any())
                    {
                        _logger.LogInformation(
                            $"Removed roles '{string.Join(", ", rolesToRemove)}' from IdentityUser with Id '{user.Id}'");
                        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    }

                    var rolesToAdd = roles?.Where(role => userRoles.Contains(role) == false).ToList();
                    if (rolesToAdd != null && rolesToAdd.Any())
                    {
                        _logger.LogInformation(
                            $"Added roles '{string.Join(", ", rolesToAdd)}' to IdentityUser with Id '{user.Id}'");
                        await _userManager.AddToRolesAsync(user, rolesToAdd);
                    }

                    return OperationResult.Success;
                }

                await _userManager.AddToRolesAsync(user, roles);
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> CreateRegisterInvite(string toEmail)
        {
            try
            {
                var userToCreate = new ApplicationUser
                {
                    Email = toEmail,
                    UserName = toEmail
                };

                await _userManager.CreateAsync(userToCreate);

                var createdUser = await _userManager.FindByEmailAsync(userToCreate.Email);

                await _userManager.SetLockoutEnabledAsync(createdUser, true);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
                var emailBody =
                    $@"Accept invite: {UrlHelperExtensions.ConfirmInviteCallbackLink(createdUser.Id, code)}";
                await _emailSender.SendEmailAsync(createdUser.Email, "Accept invite", emailBody);

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> VerifyEmail(string userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new Exception("User not found.");

                await _userManager.ConfirmEmailAsync(user, code);
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IOperationResult> AcceptInvitation(string userId, string code, string password)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new Exception("User not found.");

                await _userManager.ConfirmEmailAsync(user, code);
                await _userManager.SetLockoutEnabledAsync(user, false);

                return await SetPassword(userId,
                    new SetPasswordViewModel
                    {
                        NewPassword = password,
                        ConfirmPassword = password
                    });
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        public async Task<IEnumerable<string>> GetIdentityRoles()
        {
            return await _roleManager.Roles?.Select(x => x.NormalizedName)?.ToListAsync();
        }
    }
}