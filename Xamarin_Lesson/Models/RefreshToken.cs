using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserId { get; set; }
    }
}
