namespace CMSCore.Content.Models
{
    public class Page : EntityBase
    {
        private PageContentType _pageContentType = PageContentType.Static;
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NormalizedTitle = _title.NormalizeToSlug();
            }
        }

        public string NormalizedTitle { get; set; }
        public string Description { get; set; }

        public PageContentType PageContentType
        {
            get => _pageContentType;
            set
            {
                _pageContentType = value;

                if (_pageContentType == PageContentType.Blog)
                    Blog = Blog ?? new Blog();
                else
                    StaticContent = StaticContent ?? new StaticContent();
            }
        }

        public virtual StaticContent StaticContent { get; set; }
        public virtual Blog Blog { get; set; }
    }
}