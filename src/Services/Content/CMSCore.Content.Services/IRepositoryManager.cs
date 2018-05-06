using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Types.Results;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces.Types;

namespace CMSCore.Content.Services
{
    public interface IRepositoryManager
    {
        Task<IOperationResult> UpdatePage(UpdatePageViewModel viewModel, string entityId,
            string currentUserId);

        Task<IOperationResult> UpdateFeed(UpdateFeedViewModel viewModel, string entityId,
            string currentUserId);

        Task<IOperationResult> UpdateFeedItem(UpdateFeedItemViewModel viewModel, string entityId,
            string currentUserId);

        Task<IOperationResult> UpdateUser(UpdateUserViewModel viewModel, string entityId,
            string currentUserId);

        Task<IOperationResult> CreateAsync<T>(T entity, string currentUserId) where T : EntityBase;

        Task<IOperationResult> DeleteAsync<TEntity>(string entityId, string currentUserId) where TEntity : EntityBase;

        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBase;

        Task<IOperationResult> DeleteFeedAsync(string entityId, string currentUserId);
        Task<IOperationResult> DeleteFeedItemAsync(string entityId, string currentUserId);
        Task<IOperationResult> DeletePageAsync(string entityId, string currentUserId); 
        
        Task<IOperationResult> ConfirmDeleteFeedAsync(string entityId, string currentUserId);
        Task<IOperationResult> ConfirmDeleteFeedItemAsync(string entityId, string currentUserId,
            bool saveChanges = true);
        Task<IOperationResult> ConfirmDeletePageAsync(string entityId, string currentUserId);
    }
}