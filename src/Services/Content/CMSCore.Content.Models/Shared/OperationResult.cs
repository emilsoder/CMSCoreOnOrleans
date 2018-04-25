using System.Collections.Generic;

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
        public UpdateOperation(string userId, string entityId, TEntity entity)
        {
            UserId = userId;
            EntityId = entityId;
            Entity = entity;

            //Entity.EntityHistory.Add(new EntityHistory(entity.Id, userId, OperationType.Update));
        }

        public string UserId { get; set; }
        public string EntityId { get; set; }
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

        public DeleteOperation(string userId, string entityId, TEntity entity)
        {
            UserId = userId;
            EntityId = entityId;
            Entity = entity;

            Entity.EntityHistory.Add(new EntityHistory(entity.Id, userId, OperationType.Delete));
        }

        public string UserId { get; set; }
        public string EntityId { get; set; }
        public TEntity Entity { get; set; }
    }

    public static class OperationExtensions
    {
        public static TEntity AddDeleteHistory<TEntity>(this TEntity entity, string userId)
            where TEntity : EntityBase
        {
            entity.EntityHistory.Add(new EntityHistory(entity.Id, userId, OperationType.Delete));
            return entity;
        }
    }
}