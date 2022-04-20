using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    internal class AlbumManager : IAlbumManager
    {
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string getImagesUrl = "";

        public event EventHandler<string> RefreshToken;
        public event EventHandler<List<Image>> SuccessfulGetImage;


        public AlbumManager()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task GetUserImages(User user, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonConvert.SerializeObject(user);
            var response = await client.PostAsync(getImagesUrl, new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var content = await response.Content.ReadAsStringAsync();
                SuccessfulGetImage?.Invoke(null, JsonConvert.DeserializeObject<List<Image>>(content));
                return;
            }
            if (response.StatusCode == HttpStatusCode.Forbidden) 
            {
                RefreshToken?.Invoke(null, token);
                return;
            }

            throw new Exception("Network Error");
        }
    }
}
