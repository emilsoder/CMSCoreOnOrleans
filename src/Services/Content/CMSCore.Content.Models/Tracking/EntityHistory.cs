using System;

namespace CMSCore.Content.Models
{
    public class EntityHistory
    {
        public EntityHistory(){}

        public EntityHistory(string entityId, string userId, OperationType operationType)
        {
            EntityId = entityId;
            UserId = userId;
            OperationType = operationType;
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string EntityId { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public OperationType OperationType { get; set; } = OperationType.Add;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}