using System.Collections.Generic;

namespace CMSCore.Content.Api.Models.Content
{
    public class CreateBlogPostViewModel
    {
        public string BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }
        public bool IsContentMarkdown { get; set; } = true;

        public IList<string> Tags { get; set; }
    }
}