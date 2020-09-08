using PointNet.Data.Common.Repositories;
using PointNet.Data.Models;
using PointNet.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointNet.Services.Data.User
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        public UserService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public UsersViewModel GetAllUsers()
        {
            var allUsers = this.usersRepository.All();

            var model = new UsersViewModel
            {
                Users = allUsers
            };

            return model;
        }
    }
}
