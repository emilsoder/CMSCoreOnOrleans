using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions.Types.Results;
using CMSCore.Shared.Types.Content.EntityHistory;
using CMSCore.Shared.Types.Content.Feed;
using Orleans;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IContentGrain : IGrainWithGuidKey
    {
        #region READ

        Task<IEnumerable<Page>> PagesToList();
        Task<Page> PageById(string id);
        Task<Page> PageByName(string title);

        Task<IEnumerable<Feed>> FeedsToList();

        Task<IEnumerable<FeedItem>> FeedItemsToList();
        Task<IEnumerable<FeedItem>> FeedItemsByFeedId(string feedId);
        Task<FeedItem> FeedItemById(string feedItemId);

        Task<IEnumerable<EntityHistory>> EntityHistoryByEntityId(string entityId);
        Task<IEnumerable<EntityHistory>> EntityHistoryToList();

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

    public interface IContentManagerGrain : IGrainWithStringKey
    {
        Task<IOperationResult> Create(CreatePageViewModel model);
        Task<IOperationResult> Create(CreateFeedItemViewModel model);

        Task<IOperationResult> Update(UpdatePageViewModel model, string entityId);
        Task<IOperationResult> Update(UpdateFeedViewModel model, string entityId);
        Task<IOperationResult> Update(UpdateFeedItemViewModel model, string entityId);
        
        Task<IOperationResult> Delete(DeletePageViewModel model);
        Task<IOperationResult> Delete(DeleteFeedViewModel model);
        Task<IOperationResult> Delete(DeleteFeedItemViewModel model);

    }

    public interface IContentReaderGrain : IGrainWithStringKey
    {
        Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryByEntityId();
        Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryToList();

        Task<FeedItemViewModel> FeedItemById();
        Task<IEnumerable<FeedItemPreviewViewModel>> FeedItemsByFeedId();

        Task<IEnumerable<FeedViewModel>> FeedsToList();

        Task<IEnumerable<PageTreeViewModel>> PagesToList();
        Task<PageViewModel> PageById();
        Task<PageViewModel> PageByName();
    }

}