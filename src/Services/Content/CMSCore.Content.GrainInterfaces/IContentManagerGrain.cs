using System.Threading.Tasks;
using CMSCore.Shared.Abstractions.Types.Results;
using Orleans;
using CreateFeedItemViewModel = CMSCore.Content.GrainInterfaces.Types.CreateFeedItemViewModel;
using CreatePageViewModel = CMSCore.Content.GrainInterfaces.Types.CreatePageViewModel;
using DeleteFeedItemViewModel = CMSCore.Content.GrainInterfaces.Types.DeleteFeedItemViewModel;
using DeleteFeedViewModel = CMSCore.Content.GrainInterfaces.Types.DeleteFeedViewModel;
using DeletePageViewModel = CMSCore.Content.GrainInterfaces.Types.DeletePageViewModel;
using UpdateFeedItemViewModel = CMSCore.Content.GrainInterfaces.Types.UpdateFeedItemViewModel;
using UpdateFeedViewModel = CMSCore.Content.GrainInterfaces.Types.UpdateFeedViewModel;
using UpdatePageViewModel = CMSCore.Content.GrainInterfaces.Types.UpdatePageViewModel;

namespace CMSCore.Content.GrainInterfaces
{
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
}