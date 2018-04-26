using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
 
    public static class FeedExtensions
    {
        public static Feed UpdateFeed(this UpdateFeedViewModel model)
        {
            return new Feed()
            {
                Id = model.Id,
                Name = model.Name
            };
        }
       
        public static FeedViewModel ViewModel(this Feed model)
        {
            return new FeedViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedName = model.NormalizedName,
                FeedItems = model.FeedItems.ViewModel()
            };
        }
    }
}