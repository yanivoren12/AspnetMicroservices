using Oredering.Application.Models;
using System.Threading.Tasks;

namespace Oredering.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
