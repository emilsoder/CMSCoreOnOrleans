using System;

namespace CMSCore.Content.Models
{
    public class RemovedEntity
    {
        public RemovedEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public RemovedEntity(string entityId, string removedByUserId) : this()
        {
            RemovedEntityId = entityId;
            RemovedByUserId = removedByUserId;
        }

        public string Id { get; set; }

        public string RemovedEntityId { get; set; }

        public string RemovedByUserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}