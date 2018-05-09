using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;

namespace CMSCore.Content.Grains.Extensions
{
    internal static class ContentReaderExtensions
    {
        internal static PageViewModel ConvertToPageViewModel(Page page)
        {
            if (page == null) return null;
            var pvm = new PageViewModel
            {
                Id = page.Id,
                Content = page.StaticContent.Content,
                Name = page.Name,
                NormalizedName = page.NormalizedName,
                IsContentMarkdown = page.StaticContent.IsContentMarkdown
            };

            var feedViewModel = GetFeedViewModel(page.Feed);

            pvm.Feed = feedViewModel;
            return pvm;
        }

        internal static FeedViewModel GetFeedViewModel(Feed feed)
        {
            if (feed == null) return null;
            var fvm = new FeedViewModel
            {
                Id = feed.Id,
                Name = feed.Name,
                NormalizedName = feed.NormalizedName
            };

            if (feed.FeedItems == null || !feed.FeedItems.Any()) return fvm;

            var feedItems = GetFeedItemPreviewModels(feed.FeedItems);
            fvm.FeedItems = feedItems;

            return fvm;
        }

        internal static IEnumerable<PageTreeViewModel> GetPageTreeViewModels(IEnumerable<Page> pages)
        {
            var enumerable = pages?.ToList();
            if (enumerable == null || !enumerable.Any()) return null;
            return enumerable.Select(page => new PageTreeViewModel
                {
                    Id = page.Id,
                    Name = page.Name,
                    NormalizedName = page.NormalizedName
                })
                .ToList();
        }

        internal static IEnumerable<FeedItemPreviewViewModel> GetFeedItemPreviewModels(IEnumerable<FeedItem> feedItems)
        {
            var enumerable = feedItems?.ToList();
            if (enumerable == null || !enumerable.Any()) return null;
            var feedItemsViewModels = new List<FeedItemPreviewViewModel>();

            foreach (var item in enumerable)
            {
                feedItemsViewModels.Add(new FeedItemPreviewViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    NormalizedTitle = item.NormalizedTitle,
                     
                    Tags = item.Tags?.Select(tag => tag.Name).ToArray()
                });
            }

            return feedItemsViewModels;
        }

        internal static FeedItemViewModel GetFeedItemViewModel(FeedItem feedItem)
        {
            if (feedItem == null) return null;

            var tagArray = feedItem.Tags?.Select(tag => tag.Name).ToArray() ?? new string[] { };

            var vm = new FeedItemViewModel
            {
                Id = feedItem.Id,
                Content = feedItem.StaticContent.Content,
                Tags = tagArray,
                Description = feedItem.Description,
                Title = feedItem.Description,
                isContentMarkdown = feedItem.StaticContent.IsContentMarkdown,
                CommentsEnabled = feedItem.CommentsEnabled,
                NormalizedTitle = feedItem.NormalizedTitle,
                FeedId = feedItem.FeedId,
                Comments = new List<CommentViewModel>()
            };
            if (feedItem.CommentsEnabled && feedItem.Comments != null && feedItem.Comments.Any())
            {
                vm.Comments = feedItem.Comments?.Select(GetCommentViewModel).ToList();
            }

            return vm;
        }

        internal static CommentViewModel GetCommentViewModel(Comment comment)
        {
            if (comment == null) return null;

            return new CommentViewModel
            {
                FullName = comment.FullName,
                Text = comment.Text
            };
        }

        internal static IEnumerable<EntityHistoryViewModel> GetEntityHistoryViewModels(
            IEnumerable<EntityHistory> models)
        {
            return models?.Select(ViewModel).ToList();
        }

        private static EntityHistoryViewModel ViewModel(EntityHistory model)
        {
            return new EntityHistoryViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                EntityId = model.EntityId,
                OperationType = model.OperationType,
                Date = model.Date,
                OperationTypeName = model.OperationType.ToString()
            };
        }
    }
}