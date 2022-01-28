using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    public class Token
    {
        public string JwtToken { get; set; }
        [Ignore]
        public RefreshToken RefreshToken { get; set; }
    }
}
