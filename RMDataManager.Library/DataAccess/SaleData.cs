using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData productData;
        private readonly ISqlDataAccess sqlDataAccess;

        public SaleData(IProductData productData, ISqlDataAccess sqlDataAccess)
        {
            this.productData = productData;
            this.sqlDataAccess = sqlDataAccess;
        }
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            // TODO: Make this solid/dry/better
            // start filling in models we will save to the db
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = ConfigHelper.GetTaxRate() / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // get info about this product from db
                var productInfo = productData.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            // create sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            // save the sale model
            try
            {
                sqlDataAccess.StartTransaction("RMDatabase");
                sqlDataAccess.SaveDataInTransaction("dbo.spSale_Insert", sale);

                // get id from sale model
                sale.Id = sqlDataAccess.LoadDataInTransaction<int, dynamic>
                    ("spSale_Lookup", new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }).FirstOrDefault();

                // assign saleId to each sale detail
                foreach (var item in details)
                {
                    item.SaleId = sale.Id;

                    // save the sale detail model
                    // TODO: change this to save a whole table at once, advanced DAPPER, doing what its currently doing may be more efficient?
                    sqlDataAccess.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                }

                sqlDataAccess.CommitTransaction();
            }
            catch
            {
                sqlDataAccess.RollbackTransaction();
                throw;
            }

        }

        public List<SaleReportModel> GetSaleReport()
        {
            var output = sqlDataAccess.LoadData<SaleReportModel, dynamic>("dbo.spSale_Report", new { }, "RMDatabase");

            return output;
        }
    }
}
