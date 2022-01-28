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
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;
using Xamarin_Lesson.ViewModel;

namespace Xamarin_Lesson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            // TODO: делать запрос к серверу ?
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
            
        }

        public UserPage(User user)
        {
            InitializeComponent();
            SetUserInfo(user);
            var photoes = PhotoRepository.GetPhotoes();
            foreach (var photo in photoes)
            {
                AddImageToLayout(() => CreatePhoto(photo.Path));
                var image = new Image();
            }
        }

        private void Logout_Button_Click(object sender, EventArgs e)
        {
            var user = UserRepository.GetUserById(Id.Text);
            UserRepository.RemoveUser(user);
            var authPage = new AuthorizePage();
            var signInService = new SignIn();
            var loginViewModel = new LoginViewModel(authPage, signInService);
            App.Current.MainPage = new NavigationPage(authPage);
            return;
        }

        private async void Upload_Image_Button_Click(object sender, EventArgs e)
        {
            
            var photo = await MediaPicker.CapturePhotoAsync();
            AddImageToLayout(() => CreatePhoto(photo));
            PhotoRepository.AddPhoto(new Photo { Path = photo.FullPath });
        }

        private void AddImageToLayout(Func<ImageSource> createPhotoSource) 
        {
            var image = new Image();
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
    }
}