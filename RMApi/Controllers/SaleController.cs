using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration config;

        public SaleController(IConfiguration config)
        {
            this.config = config;
        }
        // Post name has automatic routing, if post call is made to /api/sale it uses this
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(config);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "NullUserId";
            data.SaveSale(sale, userId);
        }

        // use custom url api/getsalesreport, since were not just getting a sale
        [Authorize(Roles = "Admin,Manager")]
        [Route("api/Sale/GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            /*
            if (RequestContext.Principal.IsInRole("Admin"))
            {
                // do admin stuff
            } else if (RequestContext.Principal.IsInRole("Manager"))
            {
                // do manager stuff
            }
            */
            SaleData data = new SaleData(config);
            return data.GetSaleReport();
        }
    }
}
