using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;
using CMSCore.Shared.Types.Content.Feed;

namespace CMSCore.Shared.Types.Extensions.Content
{
    public static class FeedItemExtensions
    {
        public static FeedItem CreateModel(this CreateFeedItemViewModel model)
        {
            return new FeedItem(model.Title, model.Description, model.Content, model.Tags, model.CommentsEnabled)
            {
                FeedId = model.FeedId
            };
        }

        public static FeedItem UpdateFeedItem(this UpdateFeedItemViewModel model)
        {
            return new FeedItem(model.Title, model.Description, model.Content, model.Tags, model.CommentsEnabled)
            {
                Id = model.Id
            };
        }

        public static FeedItemViewModel ViewModel(this FeedItem model)
        {
            return new FeedItemViewModel
            {
                Id = model.Id,
                Title = model.Title,
                NormalizedTitle = model.NormalizedTitle,
                Content = model.StaticContent?.Content,
                Description = model.Description,
                isContentMarkdown = model.StaticContent?.IsContentMarkdown ?? false,
                FeedId = model.FeedId,
                CommentsEnabled = model.CommentsEnabled,
                Tags = model.Tags?.Select(x => x.Name)?.ToList(),
                Comments = model.Comments?.ViewModel()
            };
        }

        public static IEnumerable<FeedItemPreviewViewModel> ViewModel(this FeedItem[] models)
        {
            return models.Select(feedItem => new FeedItemPreviewViewModel
            {
                Id = feedItem.Id,
                Title = feedItem.Title,
                NormalizedTitle = feedItem.NormalizedTitle,
                Description = feedItem.Description,
                Tags = feedItem.Tags?.Select(x => x.Name)?.ToList()
            });
        }

        public static FeedItem UpdateModel(this FeedItem entityToUpdate, UpdateFeedItemViewModel model)
        {
            entityToUpdate.Title = model.Title;
            entityToUpdate.StaticContent.Content = model.Content;
            entityToUpdate.StaticContent.IsContentMarkdown = model.IsContentMarkdown;
            entityToUpdate.CommentsEnabled = model.CommentsEnabled;
            entityToUpdate.Description = model.Description;
            entityToUpdate.Tags = entityToUpdate.Tags.AsTagCollection(model.Tags);
            return entityToUpdate;
        }
    }
}