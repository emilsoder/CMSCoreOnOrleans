using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using CMSCore.Shared.Abstractions.Types.Results;
using Orleans;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IAccountGrain : IGrainWithGuidKey
    {
        Task<IOperationResult> Create(CreateOperation<User> model);
        Task<IOperationResult> Update(UpdateOperation<User> model);
        Task<IOperationResult> Delete(DeleteOperation<User> model);
        Task<User> Find(string userId);
        Task<IEnumerable<User>> ToList();
    }
}