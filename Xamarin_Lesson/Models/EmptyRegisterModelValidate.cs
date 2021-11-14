using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    class EmptyRegisterModelValidate : Validation<RegisterModel>
    {
        public EmptyRegisterModelValidate(RegisterModel model) : base(model)
        {
        }

        public override ValidateResult Validate()
        {
            return string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Email) ||
                   string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword)
                ? ValidateResult.Failed("Одно или несколько полей пусты")
                : ValidateResult.Ok();
        }
    }
}
