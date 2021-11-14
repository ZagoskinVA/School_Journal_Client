using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;
using Xamarin_Lesson.Services;

namespace Xamarin_Lesson.ViewModel
{
    class LoginViewModel
    {
        private readonly IAuthorize<LoginModel> _authorize;
        private readonly IAuthorizationResult<LoginModel> _authorizationResult;
        public LoginViewModel(IAuthorize<LoginModel> authorize, IAuthorizationResult<LoginModel> authorizationResult)
        {
            this._authorize = authorize;
            this._authorizationResult = authorizationResult;
            _authorize.SubmitForm += Authorize_SubmitForm;
            _authorizationResult.SuccessfulAuthorization += AuthorizationResult_SuccessfulAuthorization;
            _authorizationResult.FailedAuthorization += AuthorizationResult_FailedAuthorization;
        }

        private void AuthorizationResult_FailedAuthorization(object sender, List<string> e)
        {
            _authorize.ShowErrors(e);
        }

        private void AuthorizationResult_SuccessfulAuthorization(object sender, object e)
        {
            _authorize.ShowUserPage(e as User);
        }

        private async Task Authorize_SubmitForm(object arg1, LoginModel arg2)
        {
            try
            {
                await _authorizationResult.Authorize(arg2);
            }
            catch (Exception e)
            {
                _authorize.ShowSystemMessage("Ошибка", e.Message);
            }
            
        }
    }
}
