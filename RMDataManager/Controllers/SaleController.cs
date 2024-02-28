using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        // Post name has automatic routing, if post call is made to /api/sale it uses this
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, userId);
        }


        // use custom url api/getsalesreport, since were not just getting a sale
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
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
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
