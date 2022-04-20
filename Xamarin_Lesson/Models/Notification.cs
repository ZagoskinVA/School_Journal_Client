using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UserFrom { get; set; }
        public string EmailUserTo { get; set; }
    }
}
