
using System;

namespace CMSCore.Content.Models
{
    
    public class Page : EntityBase
    {
        public Page()
        {
            StaticContent = new StaticContent();
        }

        public Page(string name, bool feedEnabled) : this()
        {
            Name = name;
            FeedEnabled = feedEnabled;
        }

        private bool _feedEnabled = true;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NormalizedName = _name.NormalizeToSlug();
            }
        }

        public string NormalizedName { get; set; }

        public bool FeedEnabled
        {
            get => _feedEnabled;
            set
            {
                _feedEnabled = value;
                Feed = _feedEnabled ? new Feed() : null;
            }
        }

        public virtual StaticContent StaticContent { get; set; }
        public virtual Feed Feed { get; set; }
    }
}