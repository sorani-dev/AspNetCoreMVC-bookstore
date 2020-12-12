using BookStoreMvc.Models;
using System.Threading.Tasks;

namespace BookStoreMvc.Services
{
    public interface IEmailService
    {
        string GetEmailBody(string templateName);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}