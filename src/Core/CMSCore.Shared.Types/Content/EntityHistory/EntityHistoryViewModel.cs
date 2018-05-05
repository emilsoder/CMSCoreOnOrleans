using System;
using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;

namespace CMSCore.Shared.Types.Content.EntityHistory
{
    [Serializable]
    public class EntityHistoryViewModel
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        
        public OperationType OperationType { get; set; }

        public string OperationTypeName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }


    public static class EntityHistoryExtensions
    {
        public static IEnumerable<EntityHistoryViewModel> ViewModel(
            this IEnumerable<CMSCore.Content.Models.EntityHistory> models)
        {
            return models?.Select(ViewModel);
        }

        private static EntityHistoryViewModel ViewModel( CMSCore.Content.Models.EntityHistory  model)
        {
            return new EntityHistoryViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                EntityId = model.EntityId,
                OperationType = model.OperationType,
                Date = model.Date,
                OperationTypeName = model.OperationType.ToString()
            };
        }
    }
}