using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Api.Models.Content;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
    public static class PageExtensions
    {
        public static IEnumerable<PageTreeViewModel> PageTree(this IEnumerable<Page> models)
        {
            return models.Select(x =>
                new PageTreeViewModel { Id = x.Id, Name = x.Name, NormalizedName = x.NormalizedName });
        }

        public static PageViewModel ViewModel(this Page model)
        {
            return new PageViewModel
            {
                Content = model.StaticContent?.Content,
                isContentMarkdown = model.StaticContent?.IsContentMarkdown ?? true,
                Id = model.Id,
                Name = model.Name,
                Feed = model.Feed.ViewModel(),
                NormalizedName = model.NormalizedName
            };
        }

        public static Page UpdatePage(this UpdatePageViewModel model)
        {
            return new Page()
            {
                Id = model.Id,

            };
        }

        public static StaticContent ViewModel(string content, bool isMarkdownEnabled)
        {
            return new StaticContent(content, isMarkdownEnabled);
        }
    }
}