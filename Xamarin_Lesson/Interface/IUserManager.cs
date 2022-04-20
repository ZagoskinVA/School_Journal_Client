using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    public interface IUserManager
    {
        Task GetUserFromApi(string email, string token);
        event EventHandler<string> RefreshToken;
        event EventHandler<User> SuccsessfulGetUser;
        Task ConfirmFriend(string userId, string friendEmail);
    }
}
