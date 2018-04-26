using System;

namespace CMSCore.Content.Models
{
    public class RemovedEntity
    {
        public RemovedEntity() => Id = Guid.NewGuid().ToString();

        public RemovedEntity(string entityId, string userId) : this()
        {
            EntityId = entityId;
            UserId = userId;
        }

        public string Id { get; set; }

        public string EntityId { get; set; }

        public string UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}