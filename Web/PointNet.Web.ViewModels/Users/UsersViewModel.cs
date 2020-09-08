using PointNet.Data.Models;
using PointNet.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointNet.Web.ViewModels.Users
{
    public class UsersViewModel
    {
        public IQueryable<ApplicationUser> Users { get; set; }
    }
}
