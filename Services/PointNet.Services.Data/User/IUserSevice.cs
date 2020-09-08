using PointNet.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointNet.Services.Data.User
{
    public interface IUserService
    {
        public UsersViewModel GetAllUsers();
    }
}
