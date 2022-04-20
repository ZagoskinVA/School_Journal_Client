using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    public class UserManager : IUserManager
    {
        public event EventHandler<string> RefreshToken;
        public event EventHandler<User> SuccsessfulGetUser;
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string getUserUrl = "";
        private const string url = @"https://192.168.56.1:45456/api/Friend";


        public UserManager()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task GetUserFromApi(string email, string token)
        {
            var builder = new UriBuilder(getUserUrl);
            builder.Query = $"email={email}";
            var response = await client.GetAsync(builder.Uri);
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                SuccsessfulGetUser?.Invoke(null, user);
                return;
            }
            if (response.StatusCode == HttpStatusCode.Forbidden) 
            {
                RefreshToken?.Invoke(null, token);
                return;
            }
            throw new Exception("Network Error");
        }

        public async Task ConfirmFriend(string userId, string friendEmail)
        {
            var json = JsonConvert.SerializeObject(new { userId = userId, friendId = friendEmail});
            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return;
            }
        }
    }
}
