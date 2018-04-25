using System;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Models
{
    public class EntityHistoryViewModel
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public OperationType OperationType { get; set; }
        public string OperationTypeName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}