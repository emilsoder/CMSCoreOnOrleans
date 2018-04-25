using CMSCore.Content.Api.Models.Content;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
    public static class PageExtensions
    {
        public static Page ToModel(this CreatePageViewModel model)
        {
            return new Page
            {
                Title = model.Title,
                PageContentType = model.PageContentType,
                Description = model.Description
            };
        }

        public static Page ToModel(this UpdatePageViewModel model)
        {
            return new Page
            {
                Title = model.Title,
                Description = model.Description,
                StaticContent = model.Content?.ToModel()
            };
        }
    }
}