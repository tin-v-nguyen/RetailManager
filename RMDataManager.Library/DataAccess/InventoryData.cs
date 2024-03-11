using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "RMDatabase");
            return output;
        }
        
        public void SaveInventoryRecord(InventoryModel record)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData<InventoryModel>("dbo.spInventory_Insert", record, "RMDatabase");
        }
    }
}
