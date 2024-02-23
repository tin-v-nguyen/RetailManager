using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMDataManager.Models
{
    public class SaleModel
    {
        // dont initialize, want to be able to know if it is null when received from front end
        public List<SaleDetailModel> SaleDetails { get; set; }
    }
}