using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Xamarin_Lesson.Models
{
    public class User : IEquatable<User>
    {
        [Unique, NotNull]
        public string Email { get; set; }
        [PrimaryKey, Unique]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Token { get; set; }
        public string ExpDate { get; set; }

        public bool Equals(User other)
        {
            return this.Id == other?.Id;
        }
    }
}
