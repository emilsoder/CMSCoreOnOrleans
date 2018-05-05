using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.GrainInterfaces.Types
{
    #region Read

    [Orleans.Concurrency.Immutable]
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

    #endregion

    #region Write

    public class CreateFeedItemViewModel
    {
        [Required(ErrorMessage = nameof(FeedId) + " is required")]
        public string FeedId { get; set; }

        [Required(ErrorMessage = nameof(Title) + " is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = nameof(Description) + " is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = nameof(Content) + " is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = nameof(IsContentMarkdown) + " is required")]
        public bool IsContentMarkdown { get; set; } = true;

        [Required(ErrorMessage = nameof(CommentsEnabled) + " is required")]
        public bool CommentsEnabled { get; set; } = true;

        public IList<string> Tags { get; set; }
    }

    public class UpdateFeedItemViewModel
    {
        [Required(ErrorMessage = nameof(Id) + " is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = nameof(Title) + " is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = nameof(Description) + " is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = nameof(Content) + " is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = nameof(IsContentMarkdown) + " is required")]
        public bool IsContentMarkdown { get; set; } = true;

        [Required(ErrorMessage = nameof(CommentsEnabled) + " is required")]
        public bool CommentsEnabled { get; set; } = true;

        public IList<string> Tags { get; set; }
    }

    public class DeleteFeedItemViewModel
    {
        public DeleteFeedItemViewModel(string entityId)
        {
            Id = entityId;
        }

        public string Id { get; set; }

        public static DeleteFeedItemViewModel Initialize(string entityId) => new DeleteFeedItemViewModel(entityId);
    }

    #endregion
}