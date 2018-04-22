using System;
using System.Collections.Generic;
using System.Linq;

namespace CMSCore.Content.Models.Shared
{
    public class CreateOperation<TEntity>
        where TEntity : EntityBase
    {
        public CreateOperation(string userId, TEntity entity)
        {
            UserId = userId;
            Entity = entity;
            Entity.EntityHistory = new List<EntityHistory>
            {
                new EntityHistory(entity.Id, userId, OperationType.Add)
            };
        }

        public string UserId { get; set; }
        public TEntity Entity { get; set; }
    }


    public class UpdateOperation<TEntity>
        where TEntity : EntityBase
    {
        public UpdateOperation(string userId, TEntity entity)
        {
            UserId = userId;
            Entity = entity;
        }

        public string UserId { get; set; }
        public TEntity Entity { get; set; }
    }

    public class DeleteOperation<TEntity>
        where TEntity : EntityBase
    {
        public DeleteOperation(string userId, string entityId)
        {
            UserId = userId;
            EntityId = entityId;
        }

        public string UserId { get; set; }
        public string EntityId { get; set; }
        public Type EntityType => typeof(TEntity);
    }
}