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
    internal class NotificaionService : INotificationService
    {
        private HttpClientHandler httpHandler;
        private HttpClient client;
        private const string url = @"https://192.168.56.1:45456/api/Notification";
        public NotificaionService()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }
        public async Task<IEnumerable<Notification>> GetNotifications(string email)
        {
            var response = await client.GetAsync($"{url}?email={email}");
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Notification>>(content);
            }
            return null;
        }

        public async Task SendNotification(string userFrom, string userEmailTo, string message)
        {
            var notification = new Notification { Title = "Запрос в друзья", UserFrom = userFrom, EmailUserTo = userEmailTo, Message = "" };
            var json = JsonConvert.SerializeObject(notification);
            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
                return;
        }
    }
}
