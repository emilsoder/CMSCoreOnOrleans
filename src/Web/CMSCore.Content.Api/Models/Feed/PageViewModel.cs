using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
    public class PageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        #region StaticContent

        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;

        #endregion

        #region Feed

        public FeedViewModel Feed { get; set; }

        #endregion
    }

    public class PageTreeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }

    public class UpdatePageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool isContentMarkdown { get; set; } = true;
    }

}