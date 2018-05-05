using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using UserViewModel = CMSCore.Content.GrainInterfaces.Types.UserViewModel;

namespace CMSCore.Content.GrainInterfaces
{
    public interface IAccountReaderGrain : IGrainWithStringKey
    {
        Task<UserViewModel> UserById();
        Task<List<UserViewModel>> UsersToList();
    }
}