using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Types.Results;
using Orleans;
using CreateUserViewModel = CMSCore.Content.GrainInterfaces.Types.CreateUserViewModel;
using DeleteUserViewModel = CMSCore.Content.GrainInterfaces.Types.DeleteUserViewModel;
using UpdateUserViewModel = CMSCore.Content.GrainInterfaces.Types.UpdateUserViewModel;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IAccountManagerGrain : IGrainWithStringKey
    {
        Task<IOperationResult> Create(CreateUserViewModel model);
        Task<IOperationResult> Update(UpdateUserViewModel model, string entityId);
        Task<IOperationResult> Delete(DeleteUserViewModel model);
    }
}