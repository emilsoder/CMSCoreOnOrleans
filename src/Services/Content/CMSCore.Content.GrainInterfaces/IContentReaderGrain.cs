using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces.Types;
using Orleans;
using EntityHistoryViewModel = CMSCore.Content.GrainInterfaces.Types.EntityHistoryViewModel;
using FeedItemPreviewViewModel = CMSCore.Content.GrainInterfaces.Types.FeedItemPreviewViewModel;
using FeedItemViewModel = CMSCore.Content.GrainInterfaces.Types.FeedItemViewModel;
using FeedViewModel = CMSCore.Content.GrainInterfaces.Types.FeedViewModel;
using PageTreeViewModel = CMSCore.Content.GrainInterfaces.Types.PageTreeViewModel;
using PageViewModel = CMSCore.Content.GrainInterfaces.Types.PageViewModel;

namespace CMSCore.Content.GrainInterfaces
{
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

        Task<bool> CreateComment(CommentViewModel comment);
    }
}