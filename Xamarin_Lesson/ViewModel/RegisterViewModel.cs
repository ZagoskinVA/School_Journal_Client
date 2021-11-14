using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.ViewModel
{
    class RegisterViewModel
    {
        private readonly IAuthorize<RegisterModel> _authorize;
        private readonly IAuthorizationResult<RegisterModel> _authorizationResult;

        public RegisterViewModel(IAuthorize<RegisterModel> authorize, IAuthorizationResult<RegisterModel> authorizationResult)
        {
            _authorize = authorize;
            _authorizationResult = authorizationResult;
            _authorize.SubmitForm += Authorize_SubmitForm;
            _authorizationResult.FailedAuthorization += AuthorizationResult_FailedAuthorization;
            _authorizationResult.SuccessfulAuthorization += AuthorizationResult_SuccessfulAuthorization;
        }

        private void AuthorizationResult_SuccessfulAuthorization(object sender, object e)
        {
            _authorize.ShowSystemMessage("Подтверждение регистрации", e.ToString());
        }

        private void AuthorizationResult_FailedAuthorization(object sender, List<string> e)
        {
            _authorize.ShowErrors(e);
        }

        private async Task Authorize_SubmitForm(object arg1, RegisterModel arg2)
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
