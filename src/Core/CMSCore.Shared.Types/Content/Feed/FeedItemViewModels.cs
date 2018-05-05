using System;
using System.Collections.Generic;

namespace CMSCore.Shared.Types.Content.Feed
{
    [Serializable]
    public class CommentViewModel
    {
        public string Text { get; set; }
        public string FullName { get; set; }
    }
    [Serializable]
    public class CreateFeedItemViewModel
    {
        public string FeedId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;

        public bool CommentsEnabled { get; set; } = true;
        public IList<string> Tags { get; set; }
    }
    [Serializable]
    public class UpdateFeedItemViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }
        public bool IsContentMarkdown { get; set; } = true;

        public bool CommentsEnabled { get; set; } = true;
        public IList<string> Tags { get; set; }
    }
    [Serializable]
    public class FeedItemViewModel
    {
        public string Id { get; set; }
        public string FeedId { get; set; }

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;

        public bool CommentsEnabled { get; set; } = true;

        public IList<string> Tags { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
    [Serializable]
    public class DeleteFeedItemViewModel
    {
        public DeleteFeedItemViewModel(string entityId)
        {
            Id = entityId;
        }

        public string Id { get; set; }

        public static DeleteFeedItemViewModel Initialize(string entityId) => new DeleteFeedItemViewModel(entityId);
    }

}