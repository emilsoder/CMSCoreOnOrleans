namespace CMSCore.Content.Models
{
    public class StaticContent : EntityBase
    {
        public string Content { get; set; } = "No content yet...";
        public bool IsContentMarkdown { get; set; } = false;
    }
}