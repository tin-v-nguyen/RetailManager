using RMWindowsUI.Models;
using System.Threading.Tasks;

namespace RMWindowsUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}