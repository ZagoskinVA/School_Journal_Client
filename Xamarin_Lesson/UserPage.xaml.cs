using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            var user = UserRepository.GetUser();
            if (user == null || AuthorizeService.IsExpiredToken(user))
            {
                var authPage = new AuthorizePage();
                var signInService = new SignIn();
                var loginViewModel = new LoginViewModel(authPage, signInService);
                Navigation.PushModalAsync(authPage);
                return;
            }
            InitializeComponent();
            Id.Text = user.Id;
            Name.Text = $"Ваше имя {user.Name}";
            
        }

        public UserPage(User user)
        {
            InitializeComponent();
            Id.Text = user.Id;
            Name.Text = $"Ваше имя {user.Name}";
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
    }
}