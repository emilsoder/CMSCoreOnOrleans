using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.GrainInterfaces.Types
{
    #region Read

    [Orleans.Concurrency.Immutable]
    public class PageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public string Content { get; set; }
        public bool IsContentMarkdown { get; set; } = true;

        public FeedViewModel Feed { get; set; }
    }

    [Orleans.Concurrency.Immutable]
    public class PageTreeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }

    #endregion

    #region Write 

    public class UpdatePageViewModel
    {
        [Required(ErrorMessage = nameof(Id) + " is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = nameof(Name) + " is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = nameof(Content) + " is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = nameof(IsContentMarkdown) + " is required")]
        public bool IsContentMarkdown { get; set; } = true;

        [Required(ErrorMessage = nameof(FeedEnabled) + " is required")]
        public bool FeedEnabled { get; set; } = true;
    }

    public class CreatePageViewModel
    {
        [Required(ErrorMessage = nameof(Name) + " is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = nameof(Content) + " is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = nameof(IsContentMarkdown) + " is required")]
        public bool IsContentMarkdown { get; set; } = true;

        [Required(ErrorMessage = nameof(FeedEnabled) + " is required")]
        public bool FeedEnabled { get; set; } = true;
    }

    public class DeletePageViewModel
    {
        public DeletePageViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public static DeletePageViewModel Initialize(string id) => new DeletePageViewModel(id);
    }

    #endregion
}