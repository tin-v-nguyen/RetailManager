using RMWindowsUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMWindowsUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
    }
}