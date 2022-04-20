using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms.Internals;
using Xamarin_Lesson.DataBase;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    class SignIn : IAuthorizationResult<LoginModel>
    {
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string loginUrl = "https://192.168.56.1:49161/api/Authentication/SignIn";
        public event EventHandler<List<string>> FailedAuthorization;
        public event EventHandler<object> SuccessfulAuthorization;

        public SignIn()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task Authorize(LoginModel model)
        {
            var errors = Validate(model);
            if (errors != null)
            {
                FailedAuthorization?.Invoke(null, errors);
                return;
            }

            
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync(loginUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            if ((response.StatusCode != HttpStatusCode.OK))
            {
                errors = JsonConvert.DeserializeObject<ServerErrors>(content)?.ConvertToListString();
                FailedAuthorization?.Invoke(null, errors ?? new List<string>{"Ошибка с сетью"});
                return;
            }
            var token = JsonConvert.DeserializeObject<Token>(content);
            TokenRepository.AddToken(token, token.RefreshToken);
            // TODO: Запрос к другому api
            var user = await GetUser(token.JwtToken);
            UserRepository.AddUser(user);
            SuccessfulAuthorization?.Invoke(null, user);
        }

        private List<string> Validate(LoginModel model)
        {
            var validationsResult = new List<Validation> { new LoginValidate(model) }.Select(x => x.Validate());
            var validateResults = validationsResult.ToList();
            if (validateResults.All(x => x.Status == ValidateStatus.Ok))
                return null;
            var errors = new List<string>();
            validateResults.Where(x => x.Status == ValidateStatus.Failed).ForEach(x => errors.AddRange(x.ValidationErrors));
            return errors;
        }

        private async Task<User> GetUser(string token) 
        {
            const string url = @"https://192.168.56.1:45456/api/User";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var content =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(content);
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
