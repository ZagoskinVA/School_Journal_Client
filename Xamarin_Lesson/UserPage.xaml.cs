using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Lesson.DataBase;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;
using Xamarin_Lesson.ViewModel;
using Image = Xamarin_Lesson.Models.Image;

namespace Xamarin_Lesson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private readonly IPhotoService photoService;
        private readonly INotificationService notificationService;
        public UserPage(IPhotoService photoService, INotificationService notificationService)
        {
            // TODO: делать запрос к серверу ?
            if(photoService == null)
                throw new ArgumentNullException(nameof(photoService));
            if(notificationService == null)
                throw new ArgumentException(nameof(notificationService));
            this.notificationService = notificationService;
            this.photoService = photoService;
            var user = UserRepository.GetUser();
            if (user == null)
            {
                var authPage = new AuthorizePage();
                var signInService = new SignIn();
                var loginViewModel = new LoginViewModel(authPage, signInService);
                Navigation.PushModalAsync(authPage);
                return;
            }
            InitializeComponent();
            SetUserInfo(user);
            var photoes = PhotoRepository.GetPhotoes();
            foreach (var photo in photoes) 
            {
                AddImageToLayout(() => CreatePhoto(photo.Path));
            }
            Task.Run(async () =>
            {
                foreach (var photo in await photoService.DownloadPhotoes(user.Id))
                {
                    PhotoRepository.AddPhoto(photo);
                }
            });
            
        }

        public UserPage(User user, IPhotoService photoService, INotificationService notificationService)
        {
            if(photoService == null)
                throw new ArgumentNullException(nameof(photoService));
            this.photoService = photoService;
            this.notificationService = notificationService;          
            InitializeComponent();
            SetUserInfo(user);
            var photoes = PhotoRepository.GetPhotoes();
            foreach (var photo in photoes)
            {
                AddImageToLayout(() => CreatePhoto(photo.Path));
                //var image = new Image();
            }
            Task.Run(async () =>
            {
                foreach (var photo in await photoService.DownloadPhotoes(user.Id))
                {
                    PhotoRepository.AddPhoto(photo);
                }
            });
        }

        private void Logout_Button_Click(object sender, EventArgs e)
        {
            var photoes = PhotoRepository.GetPhotoes();
            foreach (var photo in photoes)
            {
                AddImageToLayout(() => CreatePhoto(photo.Path));
            }

            /*
            var user = UserRepository.GetUserById(Id.Text);
            UserRepository.RemoveUser(user);
            var authPage = new AuthorizePage();
            var signInService = new SignIn();
            var loginViewModel = new LoginViewModel(authPage, signInService);
            App.Current.MainPage = new NavigationPage(authPage);
            return; */
        }

        private async void Upload_Image_Button_Click(object sender, EventArgs e)
        {
            
            var photo = await MediaPicker.CapturePhotoAsync();
            var user = UserRepository.GetUserById(Id.Text);
            var task = photoService.UploadPhoto(photo, user);
            AddImageToLayout(() => CreatePhoto(photo));
            PhotoRepository.AddPhoto(new Photo { Path = photo.FullPath });
            await task;
        }

        private void AddImageToLayout(Func<ImageSource> createPhotoSource) 
        {
            var image = new Xamarin.Forms.Image();
            image.Source = createPhotoSource();
            image.WidthRequest = 100;
            image.HeightRequest = 100;
            PhotoesStorage.Children.Add(image);
        }

        private ImageSource CreatePhoto(string path) 
        {
            return ImageSource.FromFile(path);
        }

        private ImageSource CreatePhoto(FileResult photo) 
        {
            return ImageSource.FromStream(async x => await photo.OpenReadAsync());
        }

        private void SetUserInfo(User user) 
        {
            Id.Text = user.Id;
            Name.Text = $"Ваше имя {user.Name}";
        }

        private async void Check_Notifications_Button_Click(object sender, EventArgs e)
        {
            var user = UserRepository.GetUser();
            var notifications = await notificationService.GetNotifications(user.Email);
            if (notifications != null && notifications.Count() > 0) 
            {
                Navigation.PushModalAsync(new NotificationModal(notificationService, notifications, new UserManager()));
            }
        }

        private void Send_Notification_Button_Click(object sender, EventArgs e)
        {
            var user = UserRepository.GetUser();
            notificationService.SendNotification(user.Id, Notification.Text, "");
        }
    }
}