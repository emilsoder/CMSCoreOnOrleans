using CMSCore.Shared.Configuration;

namespace CMSCore.Identity.Extensions
{
    public class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(string userId, string code)
        {
            return $"{WebRouteEndpointsConst.BaseUrl}/api/identity/Confirm/{userId}/{code}";
        }

        public static string ResetPasswordCallbackLink(string userId, string code)
        {
            return $"{WebRouteEndpointsConst.BaseUrl}/api/identity/reset/{userId}/{code}";
        }

        public static string ConfirmInviteCallbackLink(string userId, string code)
        {
            return $"{WebRouteEndpointsConst.BaseUrl}/api/identity/confirminvite/{userId}/{code}";
        }
    }
}