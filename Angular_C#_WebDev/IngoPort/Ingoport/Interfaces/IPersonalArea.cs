using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface IPersonalArea
    {
        string GetUserInfo(int id);

        string GetUserTeam(int id);

        string GetUserTasks(int id);

        string GetUserBookmarks(int id);

        void AddTasks(int id, UserTask json);

        void ChangeTask(UserTask json);

        void DeleteTask(int id);

        string GetTask(User user);

        string GetBookmarks(User user);

        Dictionary<string, double> UserRating(int id);
    }
}
