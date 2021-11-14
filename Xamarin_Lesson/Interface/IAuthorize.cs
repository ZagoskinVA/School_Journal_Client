using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    public interface IAuthorize<T>
    {
        event Func<object, T, Task> SubmitForm;
        void ShowErrors(List<string> errors);
        void ShowUserPage(User user);
        void ShowSystemMessage(string title, string message);

    }
}
