using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    class LoginValidate : Validation<LoginModel>
    {
        public LoginValidate(LoginModel model) : base(model)
        {
        }

        public override ValidateResult Validate()
        {
            return (!string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password)) ? ValidateResult.Ok() : ValidateResult.Failed("Поле логин и пароль обязательны");
        }
    }
}
