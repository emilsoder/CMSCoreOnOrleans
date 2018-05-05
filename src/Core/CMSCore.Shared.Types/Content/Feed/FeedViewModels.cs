using System;
using System.Collections.Generic;

namespace CMSCore.Shared.Types.Content.Feed
{
    [Serializable]
    public class FeedItemPreviewViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }

        public string Description { get; set; }

        public IList<string> Tags { get; set; }
    }
    [Serializable]
    public class UpdateFeedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class FeedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public IEnumerable<FeedItemPreviewViewModel> FeedItems { get; set; }
    }
    [Serializable]
    public class DeleteFeedViewModel
    {
        public DeleteFeedViewModel(string entityId)
        {
            Id = entityId;
        }

        public string Id { get; set; }

        public static DeleteFeedViewModel Initialize(string entityId) => new DeleteFeedViewModel(entityId);
    }
}