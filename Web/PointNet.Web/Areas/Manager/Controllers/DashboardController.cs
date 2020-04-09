namespace PointNet.Web.Areas.Manager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PointNet.Common;

    [Authorize(Roles = GlobalConstants.ManagerRoleName)]
    [Area("Manager")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
