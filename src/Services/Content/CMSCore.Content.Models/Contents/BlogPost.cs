using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSCore.Content.Models
{
    public class BlogPost : EntityBase
    {
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

        public virtual StaticContent Content { get; set; } = new StaticContent();

        public string BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual List<BlogPostTag> BlogPostTags { get; set; }
    }
}