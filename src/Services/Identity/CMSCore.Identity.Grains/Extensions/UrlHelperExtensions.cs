using CMSCore.Shared.Configuration;

namespace CMSCore.Identity.Grains.Extensions
{
    public class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(string userId, string code) =>
            $"{WebRouteEndpointsConst.BaseUrl}/api/identity/Confirm/{userId}/{code}";

        public static string ResetPasswordCallbackLink(string userId, string code) =>
            $"{WebRouteEndpointsConst.BaseUrl}/api/identity/reset/{userId}/{code}";

        public static string ConfirmInviteCallbackLink(string userId, string code) =>
            $"{WebRouteEndpointsConst.BaseUrl}/api/identity/confirminvite/{userId}/{code}";
    }
}