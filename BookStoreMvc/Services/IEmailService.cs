using BookStoreMvc.Models;
using System.Threading.Tasks;

namespace BookStoreMvc.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendTestEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
    }
}