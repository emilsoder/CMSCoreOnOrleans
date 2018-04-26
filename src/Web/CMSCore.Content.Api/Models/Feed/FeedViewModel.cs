using System.Collections.Generic;

namespace CMSCore.Content.Api.Controllers
{
    public class FeedItemPreviewViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }

        public string Description { get; set; }

        public IList<string> Tags { get; set; }
    }

    public class UpdateFeedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
 
    public class FeedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public IEnumerable<FeedItemPreviewViewModel> FeedItems { get; set; }
    }

  
}