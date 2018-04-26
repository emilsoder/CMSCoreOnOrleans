using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Models.Content
{
    public class CreatePageViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public PageContentType PageContentType { get; set; }
    }
}