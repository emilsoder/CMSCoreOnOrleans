using System;
using System.Collections.Generic;

namespace CMSCore.Content.Models
{
    
    public class Tag : EntityBase
    {
        public Tag() { }

        public Tag(string name)  
        {
            Name = name;
        }

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
    }
}