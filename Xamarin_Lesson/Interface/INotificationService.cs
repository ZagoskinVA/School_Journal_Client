using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    public interface INotificationService
    {
        Task SendNotification(string userFrom, string userEmailTo, string message);
        Task<IEnumerable<Notification>> GetNotifications(string email);
    }
}
