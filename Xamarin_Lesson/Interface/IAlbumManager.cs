using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    internal interface IAlbumManager
    {
        Task GetUserImages(User user, string token);
        event EventHandler<string> RefreshToken;
        event EventHandler<List<Image>> SuccessfulGetImage; 
    }
}
