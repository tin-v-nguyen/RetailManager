﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserByID(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            // anonymous object, no named typed
            var p = new { Id = Id };

            // defualtconnection is defined in webconfig of api
            // dynamic type might not work across assemblies
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMDatabase");
            return output;
        }
    }
}
