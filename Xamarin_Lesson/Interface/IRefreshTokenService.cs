using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    internal interface IRefreshTokenService
    {
        Task RefreshToken(string token);
        event EventHandler<User> RefreshTokenSuccsessfuly;
        event EventHandler RefreshTokenFailed;
    }
}
