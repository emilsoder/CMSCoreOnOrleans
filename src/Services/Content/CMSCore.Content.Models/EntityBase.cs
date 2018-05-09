using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.Models
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            EntityId = Id;
            Version = 1;
            Date = DateTime.Now;
            IsActiveVersion = true;
        }

        [Key] public virtual string Id { get; set; }

        public virtual string EntityId { get; set; }

        public virtual int Version { get; set; }
        public virtual bool IsActiveVersion { get; set; }

        public virtual DateTime Date { get; set; }

        public string UserId { get; set; }
        public virtual bool Hidden { get; set; }

        public virtual bool MarkedToDelete { get; set; }
    }

    //public abstract class EntityVersion
    //{
    //    protected EntityVersion()
    //    {
    //        Id = Guid.NewGuid().ToString();
    //        EntityId = Guid.NewGuid().ToString();
    //        Version = 1.0;
    //        Date = DateTime.Now;
    //    }

    //    [Key]
    //    public virtual string Id { get; set; }

    //    public virtual string EntityId { get; set; }

    //    public virtual double Version { get;set; }
    //    public virtual bool IsActiveVersion { get; set; }

    //    public virtual DateTime Date { get; set; }

    //    public string UserId { get; set; }
    //}
}