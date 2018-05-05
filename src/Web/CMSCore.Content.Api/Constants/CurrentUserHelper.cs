using CMSCore.Content.Api.Extensions;

namespace CMSCore.Content.Api.Controllers
{
    public class CurrentUserHelper
    {
        public static string UserId =>
            // TODO: Implement user extracting logic
            AdminConst.AdminUserId;
    }
}