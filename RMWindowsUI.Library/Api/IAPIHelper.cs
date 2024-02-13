using RMWindowsUI.Models;
using System.Threading.Tasks;

namespace RMWindowsUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}