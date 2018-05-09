using System;

namespace CMSCore.Content.Models
{
    public class Comment : EntityBase
    {
        public Comment()
        {
        }

        public Comment(string feedItemId)
        {
            FeedItemId = feedItemId;
        }

        public Comment(string feedItemId, string text, string fullName) : this(feedItemId)
        {
            Text = text;
            FullName = fullName;
        }

        public string Text { get; set; }
        public string FullName { get; set; }

        public virtual FeedItem FeedItem { get; set; }
        public string FeedItemId { get; set; }
    }
}