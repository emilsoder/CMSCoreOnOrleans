using System.Collections.Generic;

namespace CMSCore.Content.Api.Models.Content
{
    public class GetPageViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public StaticContentViewModel Content { get; set; }

        public IList<GetBlogViewModel> Blog { get; set; } = null;
    }
}