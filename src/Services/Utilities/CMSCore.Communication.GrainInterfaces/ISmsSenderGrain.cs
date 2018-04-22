using System.Threading.Tasks;
using Orleans;

namespace CMSCore.Communication.GrainInterfaces
{
    public interface ISmsSenderGrain : IGrainWithGuidKey
    {
        Task SendSms(string fromNumber, string toNumber, string message);
    }
}