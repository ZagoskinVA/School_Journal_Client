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
    public partial class RestorePasswordPage : ContentPage
    {
        public RestorePasswordPage()
        {
            InitializeComponent();
        }

        private void RestorePassword_ButtonClick(object sender, EventArgs e)
        {
            var email = this.email.Text;
        }
    }
}