using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;
using CMSCore.Shared.Types.Content.Feed;

namespace CMSCore.Shared.Types.Extensions.Content
{
    public static class FeedExtensions
    {
        public static Feed UpdateFeed(this UpdateFeedViewModel model)
        {
            return new Feed
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static FeedViewModel ViewModel(this Feed model)
        {
            return new FeedViewModel
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedName = model.NormalizedName,
                FeedItems = model.FeedItems?.ToArray().ViewModel()
            };
        }

        public static IEnumerable<FeedViewModel> ViewModel(this IEnumerable<Feed> model)
        {
            return model.Select(ViewModel);
        }

        public static Feed UpdateModel(this Feed entityToUpdate, UpdateFeedViewModel model)
        {
            entityToUpdate.Name = model.Name;
            return entityToUpdate;
        }
    }
}