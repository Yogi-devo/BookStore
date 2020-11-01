using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestMail(UserEmailOptions userEmailOptions);
    }
}