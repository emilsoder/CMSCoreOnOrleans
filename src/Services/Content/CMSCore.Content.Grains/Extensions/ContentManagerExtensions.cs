using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;

namespace CMSCore.Content.Grains.Extensions
{
    internal static class ContentManagerExtensions
    {
        public static FeedItem CreateModel(this CreateFeedItemViewModel model)
        {
            return new FeedItem(model.Title, model.Description, model.Content, model.Tags, model.CommentsEnabled)
            {
                FeedId = model.FeedId
            };
        }
        public static StaticContent GetStaticContent(this CreatePageViewModel model)
        {
            return new StaticContent(model.Content, model.IsContentMarkdown);
        }

        public static Page CreateModel(this CreatePageViewModel model)
        {
            return new Page(model.Name, model.FeedEnabled)
            {
                StaticContent = model.GetStaticContent()
            };
        }
    }
}