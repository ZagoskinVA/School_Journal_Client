using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    internal interface IUserRepository
    {
        event EventHandler<User> UserChanged;
        event EventHandler<string> ShowError;
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task GetUser();
    }
}
