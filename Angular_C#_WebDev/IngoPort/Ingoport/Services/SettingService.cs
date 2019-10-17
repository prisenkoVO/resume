namespace Ingoport.Services
{
    using System.Linq;
    using Newtonsoft.Json;
    using Ingoport.Models;
    using Ingoport.Interfaces;

    public class SettingService : ISetting
    {
        public readonly UserContext UserContext;

        public SettingService(UserContext user)
        {
            this.UserContext = user;
        }

        public string GetUsers(int id)
        {
            foreach (var arr in this.UserContext.Users)
            {
                if (arr.Id == id)
                {
                    return JsonConvert.SerializeObject(arr);
                }
            }

            return string.Empty;
        }

        public void PutUserData( User getUser, int id)
        {
            using (this.UserContext)
            {
                var user = this.UserContext.Users.FirstOrDefault(e => e.Id == id);

                user.Birth = user.Birth!=getUser.Birth?getUser.Birth:user.Birth;
                user.Phone = user.Phone!=getUser.Phone?getUser.Phone:user.Phone;
                user.Email = user.Email!=getUser.Email?getUser.Email:user.Email;
                user.Photo = user.Photo!=getUser.Photo?getUser.Photo:user.Photo;


                this.UserContext.SaveChanges();
            }
        }
    }
}
