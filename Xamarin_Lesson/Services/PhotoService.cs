using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    public class PhotoService : IPhotoService
    {
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string url = @"https://192.168.56.1:45456/api/Photo";

        public PhotoService()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task<List<Photo>> DownloadPhotoes(string userId)
        {
            var response = await client.GetAsync($"{url}?userId={userId}");
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var conten = await response.Content.ReadAsStringAsync();
                var photoes = JsonConvert.DeserializeObject<List<Photo>>(conten);
                return photoes;
            }
            throw new Exception(response.StatusCode.ToString());
        }

        public async  Task UploadPhoto(FileResult photo, User user)
        {
            using (var ms = new MemoryStream()) 
            {
                (await photo.OpenReadAsync()).CopyTo(ms);
                var file = new
                {
                    UserId = user.Id,
                    Id = 0,
                    Title = photo.FileName,
                    Content = ms.ToArray().Select(x => (int)x).ToArray(),
                    IsForFamily = true,
                    Path = "",
                    Tags = ""
                };
                var json = JsonConvert.SerializeObject(file);
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    return;
                }

                throw new Exception(response.StatusCode.ToString());

            }
        }

        public Task<List<Photo>> DownloadFriendPhotoes(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
