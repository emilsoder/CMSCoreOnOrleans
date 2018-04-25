using System.Threading.Tasks;
using CMSCore.Identity.Models;
using CMSCore.Identity.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSCore.Identity.Api.Controllers
{
    //public interface IManageController
    //{
    //    string StatusMessage { get; set; }

    //    void AddErrors(IdentityResult result);
    //    Task<IActionResult> ChangePassword();
    //    Task<IActionResult> ChangePassword(ChangePasswordViewModel model);
    //    Task<IActionResult> Disable2fa();
    //    Task<IActionResult> Disable2faWarning();
    //    Task<IActionResult> EnableAuthenticator();
    //    Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model);
    //    Task<IActionResult> ExternalLogins();
    //    string FormatKey(string unformattedKey);
    //    string GenerateQrCodeUri(string email, string unformattedKey);
    //    Task<IActionResult> GenerateRecoveryCodes();
    //    Task<IActionResult> GenerateRecoveryCodesWarning();
    //    Task<IdentityUserViewModel> GetIndexViewModel();
    //    Task<IActionResult> LinkLogin(string provider);
    //    Task<IActionResult> LinkLoginCallback();
    //    Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user, EnableAuthenticatorViewModel model);
    //    Task<IActionResult> RemoveLogin(RemoveLoginViewModel model);
    //    Task<IActionResult> ResetAuthenticator();
    //    Task<bool> SendVerificationEmail(IdentityUserViewModel model);
    //    Task<IActionResult> SetPassword();
    //    Task<IActionResult> SetPassword(SetPasswordViewModel model);
    //    ShowRecoveryCodesViewModel ShowRecoveryCodes();
    //    Task<IActionResult> TwoFactorAuthentication();
    //    Task<bool> UpdateIndexViewModel(IdentityUserViewModel model);
    //}

    //public interface IManageGrain
    //{
    //    string StatusMessage { get; set; }

    //    string FormatKey(string unformattedKey);
    //    string GenerateQrCodeUri(string email, string unformattedKey);
    //    void AddErrors(IdentityResult result);


    //    Task<bool> ChangePassword(ChangePasswordViewModel model);
    //    Task<bool> Disable2fa();
    //    Task<bool> CanDisable2fa();
    //    Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model);
    //    Task<ExternalLoginsViewModel> ExternalLogins();
    //    Task<ShowRecoveryCodesViewModel> GenerateRecoveryCodes();
    //    Task<bool> CanGenerateRecoveryCodes();
    //    Task<IdentityUserViewModel> GetIndexViewModel();
    //    Task<ChallengeResult> LinkLogin(string provider);
    //    Task<bool> LinkLoginCallback();
    //    Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user, EnableAuthenticatorViewModel model);
    //    Task<bool> RemoveLogin(RemoveLoginViewModel model);
    //    Task<bool> ResetAuthenticator();
    //    Task<bool> SendVerificationEmail(IdentityUserViewModel model);
    //    Task<bool> SetPassword(SetPasswordViewModel model);
    //    ShowRecoveryCodesViewModel ShowRecoveryCodes();
    //    Task<TwoFactorAuthenticationViewModel> TwoFactorAuthentication();
    //    Task<bool> UpdateIndexViewModel(IdentityUserViewModel model);
    //}
}