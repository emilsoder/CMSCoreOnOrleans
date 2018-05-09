using System;
using System.Collections.Generic;

namespace CMSCore.Content.Models
{
    public class Feed : EntityBase
    {
        public Feed()
        {
        }

        public Feed(string pageId)
        {
            PageId = pageId;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NormalizedName = _name.NormalizeToSlug();
            }
        }

        public string NormalizedName { get; set; }

        public virtual ICollection<FeedItem> FeedItems { get; set; }

        public virtual Page Page { get; set; }
        public string PageId { get; set; }
    }
}