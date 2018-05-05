using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.GrainInterfaces.Types
{
    #region Read

    [Orleans.Concurrency.Immutable]
    public class FeedItemPreviewViewModel
    {
        private string[] _tags = new string[] { };
        public string Id { get; set; }

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public string[] Tags
        {
            get => _tags;
            set => _tags = value ?? new string[] { };
        }
    }

    [Orleans.Concurrency.Immutable]
    public class FeedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public IEnumerable<FeedItemPreviewViewModel> FeedItems { get; set; }
    }

    #endregion

    #region Write

    public class UpdateFeedViewModel
    {
        [Required(ErrorMessage = nameof(Id) + " is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = nameof(Name) + " is required")]
        public string Name { get; set; }
    }


    public class DeleteFeedViewModel
    {
        public DeleteFeedViewModel(string entityId)
        {
            Id = entityId;
        }

        public string Id { get; set; }

        public static DeleteFeedViewModel Initialize(string entityId) => new DeleteFeedViewModel(entityId);
    }

    #endregion
}