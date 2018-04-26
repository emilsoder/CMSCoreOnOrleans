using System.Collections.Generic;

namespace CMSCore.Content.Models
{
    public class FeedItem : EntityBase
    {
        public FeedItem()
        {
            StaticContent = new StaticContent();
        }

        public FeedItem(string title, string description, string content)
            : this()
        {
            Title = title;
            Description = description;
            StaticContent.Content = content;
        }

        public FeedItem(string title, string description, string content,IList<string> tagNames)
            : this(title, description, content)
        {
            Tags = Tags.AsTagCollection(tagNames);
        }

        public FeedItem(string title, string description, string content, IList<string> tagNames, bool commentsEnabled)
            : this(title, description, content, tagNames)
        {
            CommentsEnabled = commentsEnabled;
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NormalizedTitle = _title.NormalizeToSlug();
            }
        }

        public string NormalizedTitle { get; set; }
        public string Description { get; set; }

        public virtual StaticContent StaticContent { get; set; }


        public string FeedId { get; set; }
        public virtual Feed Feed { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public bool CommentsEnabled { get; set; } = true;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}