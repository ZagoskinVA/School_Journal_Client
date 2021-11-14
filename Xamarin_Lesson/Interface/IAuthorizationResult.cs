using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.Interface
{
    public interface IAuthorizationResult<T>
    {
        event EventHandler<List<string>> FailedAuthorization;
        event EventHandler<object> SuccessfulAuthorization;
        Task Authorize(T model);
    }
}
