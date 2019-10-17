using Ingoport.Models;

namespace Ingoport.Interfaces
{
    interface ISetting
    {
        string GetUsers(int id);

        void PutUserData(User getUser, int id);
    }
}
