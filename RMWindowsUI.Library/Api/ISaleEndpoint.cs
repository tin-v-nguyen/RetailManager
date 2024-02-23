using RMWindowsUI.Library.Models;
using System.Threading.Tasks;

namespace RMWindowsUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}