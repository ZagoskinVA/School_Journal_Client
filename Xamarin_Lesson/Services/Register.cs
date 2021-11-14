using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
using Xamarin_Lesson.DataBase;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    class Register : IAuthorizationResult<RegisterModel>
    {
        private HttpClientHandler httpHandler;
        private HttpClient client;
        public event EventHandler<List<string>> FailedAuthorization;
        public event EventHandler<object> SuccessfulAuthorization;
        private const string registerUrl = "https://192.168.43.157:45455/api/Authentication/Registration";

        public Register()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task Authorize(RegisterModel model)
        {
            var errors = Validate(model);
            if (errors != null)
            {
                FailedAuthorization?.Invoke(null, errors);
                return;
            }
            var json = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync(registerUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            var message = await response.Content.ReadAsStringAsync();
            if ((response.StatusCode != HttpStatusCode.OK))
            {
                errors = JsonConvert.DeserializeObject<ServerErrors>(message)?.ConvertToListString();
                FailedAuthorization?.Invoke(null, errors ?? new List<string> { "Ошибка с сетью" });
                return;
            }
            SuccessfulAuthorization?.Invoke(null, message);

        }

        private List<string> Validate(RegisterModel model)
        {
            var validationResults = new List<Validation> { new EmptyRegisterModelValidate(model), new EqualPasswordsvalidation(model) }.Select(x => x.Validate()).ToList();
            if (validationResults.All(x => x.Status == ValidateStatus.Ok))
                return null;
            var errors = new List<string>();
            validationResults.Where(x => x.Status == ValidateStatus.Failed).ForEach(x => errors.AddRange(x.ValidationErrors));
            return errors;
        }
    }
}
