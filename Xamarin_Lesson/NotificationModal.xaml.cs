using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationModal : ContentPage
    {
        private readonly INotificationService notificationService;
        private readonly IEnumerable<Notification> notifications;
        private readonly IUserManager userManager;
        public NotificationModal(INotificationService notificationService, IEnumerable<Notification> notifications, IUserManager userManager)
        {
            this.notificationService = notificationService;
            this.notifications = notifications;
            this.userManager = userManager;
            InitializeComponent();
            SetNotification();
        }

        private void SetNotification() 
        {
            foreach (var notification in notifications) 
            {
                NotificationsContainer.Children.Add(new Label() { Text = $"{notification.Title} from {notification.UserFrom}" });
            }
        }

        private void ConfirmAllNotifications(object sender, EventArgs e)
        {
            foreach (var notification in notifications) 
            {
                userManager.ConfirmFriend(notification.UserFrom, notification.EmailUserTo);
            }
        }
    }
}