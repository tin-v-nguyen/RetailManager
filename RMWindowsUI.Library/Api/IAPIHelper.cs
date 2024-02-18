using RMWindowsUI.Models;
using System.Net.Cache;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMWindowsUI.Library.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}