using System.Linq;
using System.Collections.Generic;


namespace Ingoport.Services
{
    using Models;
    using Interfaces;

    public class UsersService
    {
        private readonly IUser Users;
        


        public IEnumerable<User> GetUsers()
        {
            return this.Users.AllUsers;
        }

        //public IEnumerable<User> GetUsersByRole(long roleId)
        //{

        //    return this.Users.AllUsers.Where(item => item?.Role?.FirstOrDefault(role => role.Id == roleId) != null);
        //}
    }
}
