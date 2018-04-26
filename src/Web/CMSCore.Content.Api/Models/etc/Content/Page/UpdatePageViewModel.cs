namespace CMSCore.Content.Api.Models.Content
{
    public class UpdatePageViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public StaticContentViewModel Content { get; set; }
    }
}