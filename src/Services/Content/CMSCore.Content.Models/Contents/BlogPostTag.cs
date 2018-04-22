namespace CMSCore.Content.Models
{
    public class BlogPostTag
    {
        public string BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }

        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}