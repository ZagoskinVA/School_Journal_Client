// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;
using Xamarin_Lesson.ViewModel;

namespace Xamarin_Lesson
{
    public partial class AuthorizePage : ContentPage, IAuthorize<LoginModel>
    {
        public AuthorizePage()
        {
            InitializeComponent();
        }

        public event Func<object, LoginModel, Task> SubmitForm;

        private async void  SigIn_Button_Click(object sender, EventArgs e)
        {
            ErrorList.ItemsSource = null;
            var model = new LoginModel {Email = Email.Text, Password = Passwrod.Text};
            await SubmitForm?.Invoke(sender, model);
        }

        private async void NavigateToRegister_Button_Click(object sender, EventArgs e)
        {
            var registrationPage = new RegistrationPage();
            var registerService = new Register();
            var registerViewModel = new RegisterViewModel(registrationPage, registerService);
            await Navigation.PushModalAsync(registrationPage);
        }

        private async void RestorePassword_ButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RestorePasswordPage());
        }

        public void ShowErrors(List<string> errors)
        {
            ErrorList.ItemsSource = errors;
        }

        public void ShowUserPage(User user)
        {
            App.Current.MainPage = new NavigationPage(new UserPage(user, new PhotoService(), new NotificaionService()));
        }

        public void ShowSystemMessage(string title, string message)
        {
            DisplayAlert(title, message, "Ok");
        }
    }
}
