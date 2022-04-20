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
    internal class RefreshTokenService : IRefreshTokenService
    {
        public event EventHandler<User> RefreshTokenSuccsessfuly;
        public event EventHandler RefreshTokenFailed;
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string refreshTokenUrl = "https://192.168.56.1:49153/api/Authentication/SignIn";

        public RefreshTokenService()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }

        public async Task RefreshToken(string token)
        {
            var builder = new UriBuilder(refreshTokenUrl);
            builder.Query = $"token={token}";
            var response = await client.GetAsync(builder.Uri);
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                RefreshTokenSuccsessfuly?.Invoke(null, user);
                return;
            }
            if (response.StatusCode == HttpStatusCode.Forbidden) 
            {
                RefreshTokenFailed?.Invoke(null, EventArgs.Empty);
                return;
            }
            throw new Exception("Network Error");
        }
    }
}
