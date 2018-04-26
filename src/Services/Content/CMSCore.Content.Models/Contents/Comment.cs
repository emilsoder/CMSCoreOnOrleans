namespace CMSCore.Content.Models
{
    public class Comment : EntityBase
    {
        public Comment()
        {
            
        }
                
        public Comment(string text, string fullName)
        {
            Text = text;
            FullName = fullName;
        }
        public string Text { get; set; }
        public string FullName { get;set; }
        
        //public virtual FeedItem FeedItem { get; set; }
        //public string FeedItemId { get; set; }

    }
}