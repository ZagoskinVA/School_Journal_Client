using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;

namespace Xamarin_Lesson
{
    public partial class RegistrationPage: ContentPage, IAuthorize<RegisterModel>
    {

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void Registration_Button_Click(object sender, EventArgs e)
        {
            ErrorList.ItemsSource = null;
            var model = new RegisterModel
                { Email = email.Text, Name = name.Text, Password = password.Text, ConfirmPassword = confirmPassword.Text };
            await SubmitForm?.Invoke(sender, model);
        }

        public event Func<object, RegisterModel, Task> SubmitForm;
        public void ShowErrors(List<string> errors)
        {
            ErrorList.ItemsSource = errors;
        }

        public void ShowUserPage(User user)
        {
            var conformationEmailPage = new ConfirmeEmailPage(user.Email);
            Navigation.PushModalAsync(conformationEmailPage);
        }

        public void ShowSystemMessage(string title, string message)
        {
            DisplayAlert(title, message, "Ок");
        }
    }
}