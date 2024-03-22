using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private readonly IConfiguration config;
        private readonly ISqlDataAccess sqlDataAccess;

        public InventoryData(IConfiguration config, ISqlDataAccess sqlDataAccess)
        {
            this.config = config;
            this.sqlDataAccess = sqlDataAccess;
        }
        public List<InventoryModel> GetInventory()
        {
            var output = sqlDataAccess.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "RMDatabase");
            return output;
        }

        public void SaveInventoryRecord(InventoryModel record)
        {
            sqlDataAccess.SaveData<InventoryModel>("dbo.spInventory_Insert", record, "RMDatabase");
        }
    }
}
