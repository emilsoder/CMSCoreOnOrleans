using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions.Types.Results;
using Orleans;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IContentGrain : IGrainWithGuidKey
    {
        #region READ

        Task<IEnumerable<Page>> Pages();
        Task<Page> PageById(string id);
        Task<Page> PageByName(string title);

        Task<IEnumerable<Feed>> Feeds();

        Task<IEnumerable<FeedItem>> FeedItems();
        Task<IEnumerable<FeedItem>> FeedItems(string feedId);
        Task<FeedItem> FeedItemDetails(string feedItemId);

        Task<IEnumerable<EntityHistory>> EntityHistory(string entityId);
        Task<IEnumerable<EntityHistory>> EntityHistory();

        #endregion


        #region Create

        Task<IOperationResult> Create(CreateOperation<Page> operation);
        Task<IOperationResult> Create(CreateOperation<FeedItem> operation);

        #endregion

        #region Update

        Task<IOperationResult> Update(UpdateOperation<Page> operation);
        Task<IOperationResult> Update(UpdateOperation<Feed> operation);
        Task<IOperationResult> Update(UpdateOperation<FeedItem> operation);
        //Task<IOperationResult> Update(UpdateOperation<StaticContent> model);

        #endregion

        #region Delete

        Task<IOperationResult> Delete(DeleteOperation<Page> operation);
        Task<IOperationResult> Delete(DeleteOperation<Feed> operation);
        Task<IOperationResult> Delete(DeleteOperation<FeedItem> operation);
        //Task<IOperationResult> Delete(DeleteOperation<StaticContent> model);

        #endregion
    }
}