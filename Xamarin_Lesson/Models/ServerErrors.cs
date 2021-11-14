using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    public class ServerErrors
    {
        public List<List<string>> Errors { get; set; }

        public List<string> ConvertToListString()
        {
            var errors = new List<string>();
            foreach (var error in Errors)
            {
                errors.AddRange(error);
            }

            return errors;
        }
    }
}
