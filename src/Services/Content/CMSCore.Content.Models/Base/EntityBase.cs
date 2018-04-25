using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.Models
{
    public abstract class EntityBase
    {
        private bool _isRemoved;

        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        [Key] public virtual string Id { get; set; }

        public virtual bool IsDisabled { get; set; }

        public virtual bool IsRemoved
        {
            get => _isRemoved;
            set
            {
                _isRemoved = value;
                if (_isRemoved) IsDisabled = true;
            }
        }

        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }

        public virtual List<EntityHistory> EntityHistory { get; set; }
    }
}