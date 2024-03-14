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
    public class UserData
    {
        private readonly IConfiguration config;

        public UserData(IConfiguration config)
        {
            this.config = config;
        }
        public List<UserModel> GetUserByID(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(config);

            // anonymous object, no named typed
            var p = new { Id = Id };

            // RMDatabase is defined in webconfig of api
            // dynamic type might not work across assemblies
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMDatabase");
            return output;
        }
    }
}
