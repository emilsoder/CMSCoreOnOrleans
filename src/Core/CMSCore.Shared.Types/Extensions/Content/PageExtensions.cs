using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;
using CMSCore.Shared.Types.Content.Feed;

namespace CMSCore.Shared.Types.Extensions.Content
{
    public static class PageExtensions
    {
        public static IEnumerable<PageTreeViewModel> ViewModel(this IEnumerable<Page> models)
        {
            return models.Select(x =>
                new PageTreeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    NormalizedName = x.NormalizedName
                });
        }

        public static PageViewModel ViewModel(this Page model)
        {
            return new PageViewModel
            {
                Content = model.StaticContent?.Content,
                isContentMarkdown = model.StaticContent?.IsContentMarkdown ?? true,
                Id = model.Id,
                Name = model.Name,
                Feed = model.Feed?.ViewModel(), //TODO: wtf
                NormalizedName = model.NormalizedName
            };
        }

        public static Page UpdatePage(this UpdatePageViewModel model)
        {
            return new Page()
            {
                Id = model.Id,
                Name = model.Name,
                StaticContent = GetStaticContent(model.Content, model.IsContentMarkdown),
            };
        }

        public static StaticContent GetStaticContent(string content, bool isMarkdownEnabled)
        {
            return new StaticContent(content, isMarkdownEnabled);
        }

        public static StaticContent GetStaticContent(this CreatePageViewModel model)
        {
            return new StaticContent(model.Content, model.isContentMarkdown);
        }

        public static Page CreateModel(this CreatePageViewModel model)
        {
            return new Page(model.Name, model.FeedEnabled)
            {
                StaticContent = model.GetStaticContent()
            };
        }

        public static Page UpdateModel(this Page entityToUpdate, UpdatePageViewModel model)
        {
            entityToUpdate.Name = model.Name;
            entityToUpdate.FeedEnabled = model.FeedEnabled;
            entityToUpdate.StaticContent.Content = model.Content;
            entityToUpdate.StaticContent.IsContentMarkdown = model.IsContentMarkdown;
            return entityToUpdate;
        }
    }
}