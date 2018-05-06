using System;
using System.Threading.Tasks;

namespace CMSCore.Identity.Services
{
    
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message) => Task.CompletedTask;
    }
}
