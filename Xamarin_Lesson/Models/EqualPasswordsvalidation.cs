using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    class EqualPasswordsvalidation : Validation<RegisterModel>
    {
        public EqualPasswordsvalidation(RegisterModel model) : base(model)
        {
        }

        public override ValidateResult Validate()
        {
            return model.Password == model.ConfirmPassword
                ? ValidateResult.Ok()
                : ValidateResult.Failed("Пароли должны совпадать");
        }
    }
}
