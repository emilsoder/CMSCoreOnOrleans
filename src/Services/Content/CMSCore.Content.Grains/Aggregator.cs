using System.Threading.Tasks;
using CMSCore.Content.Models;
using EFCore.Repository;

namespace CMSCore.Content.Grains
{
    public static class Aggregator
    {
        public static async Task AggregateEntityHistory(
            this IRepository _repository,
            string entityId,
            string userId,
            OperationType operationType,
            bool saveChanges = true)
        {
            var entityHistory = new EntityHistory(entityId, userId, operationType);
            _repository.Add(entityHistory);

            if (saveChanges)
                await _repository.SaveChangesAsync();
        }
    }
}