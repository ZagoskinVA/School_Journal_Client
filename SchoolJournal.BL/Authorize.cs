using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SchoolJournal.BL
{
    public class Authorize
    {
        public HttpClient Client { get; private set; } = new HttpClient();
        private readonly string url = "https://";
    }
}
