using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Lesson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmeEmailPage : ContentPage
    {
        public ConfirmeEmailPage(string email)
        {
            InitializeComponent();
            Email.Text = email;
        }

        private async void Confirm_Email_Button_Click(object sender, EventArgs e)
        {
            
        }

    }
}