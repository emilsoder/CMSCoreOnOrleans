using System;
using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.EntityFrameworkCore;

namespace CMSCore.Content.Grains
{
    public static class DbOperationsExtensions
    {
        public static async Task<IOperationResult> DeleteEntity<T>(this DbContext _context, string entityId,
            string userId) where T : EntityBase
        {
            var entityToMarkAsDeleted = await _context.FindAsync<T>(entityId);
            if (entityToMarkAsDeleted == null)
                throw new Exception("Entity to perform delete operation could not be loaded.");

            entityToMarkAsDeleted.IsRemoved = true;
            entityToMarkAsDeleted.AddDeleteHistory(userId);

            _context.Update(entityToMarkAsDeleted);
            _context.SaveChanges();

            var removedEntity = new RemovedEntity(entityToMarkAsDeleted.Id, userId);
            _context.Add(removedEntity);

            await _context.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}