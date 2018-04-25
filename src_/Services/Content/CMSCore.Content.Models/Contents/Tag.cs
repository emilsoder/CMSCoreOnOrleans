using System.Collections.Generic;

namespace CMSCore.Content.Models
{
    public class Tag : EntityBase
    {
        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                NormalizedName = _displayName.NormalizeToSlug();
            }
        }

        public string NormalizedName { get; set; }

        public virtual List<BlogPostTag> BlogPostTags { get; set; }
    }
}