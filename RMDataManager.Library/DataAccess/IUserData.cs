﻿using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserByID(string Id);
    }
}