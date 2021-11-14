using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Services
{
    class AuthorizeService
    {
        public static User EncodeToken(JwtToken token)
        {
            var encodeToken = Jose.JWT.Decode(token.Token, Encoding.UTF8.GetBytes("NiceDickAwesomeBalls"));
            var user = JsonConvert.DeserializeObject<User>(encodeToken);
            user.Token = token.Token;
            return user;
        }

        public static bool IsExpiredToken(User user)
        {
            if (user == null)
                return true;
            user = EncodeToken(new JwtToken { Token = user.Token });
            var date = DateTime.ParseExact(user.ExpDate, "yy-MM-dd hh:mm", CultureInfo.InvariantCulture);
            var now = DateTime.Now;
            return now > date;
        }
    }
}
