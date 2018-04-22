using System;
using System.Threading.Tasks;
using CMSCore.Communication.GrainInterfaces;
using Orleans;

namespace CMSCore.Communication.Grains
{
    public class EmailSenderGrain : Grain, IEmailSenderGrain
    {
        public async Task SendEmail(string fromEmail, string toEmail, string message, string subject)
        {
        }
    }
}
