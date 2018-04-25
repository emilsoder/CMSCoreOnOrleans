using System.Collections.Generic;

namespace CMSCore.Content.Models
{
    public class Blog : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual List<BlogPost> BlogPosts { get; set; }
    }
}