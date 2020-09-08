using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PointNet.Data.Common.Repositories;
using PointNet.Data.Models;
using PointNet.Services.Data.User;
using PointNet.Web.ViewModels.Users;

namespace PointNet.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        public ChatController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Chat()
        {
            var allUsers = userService.GetAllUsers();

            return this.View(allUsers);
        }
    }
}