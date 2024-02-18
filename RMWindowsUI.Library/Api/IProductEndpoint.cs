using RMWindowsUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMWindowsUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}