using System;
using System.Threading.Tasks;
using Orleans;

namespace CMSCore.Communication.GrainInterfaces
{
    public interface IEmailSenderGrain : IGrainWithGuidKey
    {
        Task SendEmail(string fromEmail, string toEmail, string message, string subject);
    }
}
