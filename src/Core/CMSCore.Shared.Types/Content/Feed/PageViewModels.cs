using System;

namespace CMSCore.Shared.Types.Content.Feed
{
    [Serializable]
    public class PageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }


        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;


        public FeedViewModel Feed { get; set; }
    }

    [Serializable]
    public class PageTreeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }

    [Serializable]
    public class UpdatePageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsContentMarkdown { get; set; } = true;
        public bool FeedEnabled { get; set; } = true;
    }

    [Serializable]
    public class CreatePageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;
        public bool FeedEnabled { get; set; } = true;
    }

    [Serializable]
    public class DeletePageViewModel
    {
        public DeletePageViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public static DeletePageViewModel Initialize(string id) => new DeletePageViewModel(id);
    }
}