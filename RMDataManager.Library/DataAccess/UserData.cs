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
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public UserData(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }
        public List<UserModel> GetUserByID(string Id)
        {

            // anonymous object, no named typed
            var p = new { Id = Id };

            // RMDatabase is defined in webconfig of api
            // dynamic type might not work across assemblies
            var output = sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMDatabase");
            return output;
        }
    }
}
