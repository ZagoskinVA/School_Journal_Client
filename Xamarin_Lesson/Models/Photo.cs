using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public bool IsForFamily { get; set; }
        public string Tags { get; set; }
        public string Path { get; set; }
        [Ignore]
        public int[] Content { get; set; }
    }
}
