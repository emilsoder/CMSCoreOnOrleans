using System.Threading.Tasks;
using CMSCore.Communication.GrainInterfaces;
using Orleans;

namespace CMSCore.Communication.Grains
{
    public class SmsSenderGrain : Grain, ISmsSenderGrain {
        public async Task SendSms(string fromNumber, string toNumber, string message)
        {
        }
    }
}