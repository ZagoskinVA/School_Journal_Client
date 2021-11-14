using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Lesson.DataBase;
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;
using Xamarin_Lesson.ViewModel;

namespace Xamarin_Lesson
{
    public partial class App : Application
    {
        private readonly LoginViewModel loginViewModel;
        public App()
        {
            InitializeComponent();
            InitDataBase.CreateDataBase();

            MainPage = new NavigationPage(new UserPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
