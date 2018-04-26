using System.Collections.Generic;

namespace CMSCore.Content.Api.Models.Content
{
    public class UpdateBlogPostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public StaticContentViewModel Content { get; set; }

        public IList<string> Tags { get; set; }
    }
}