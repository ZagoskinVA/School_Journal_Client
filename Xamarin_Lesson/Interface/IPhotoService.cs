using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    public interface IPhotoService
    {
        Task UploadPhoto(FileResult photo, User user);
        Task<List<Photo>> DownloadPhotoes(string userId);
        Task<List<Photo>> DownloadFriendPhotoes(string userId);
    }
}
