using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    internal class ValidateResult
    {
        public ValidateStatus Status { get; private set; }
        public List<string> ValidationErrors { get; private set; } = new List<string>();
        public static  ValidateResult Ok() => new ValidateResult { Status = ValidateStatus.Ok };

        public static ValidateResult Failed(string error)
        {
            var result = new ValidateResult
            {
                Status = ValidateStatus.Failed
            };
            result.ValidationErrors.Add(error);
            return result;
        }

        public static ValidateResult Failed(List<string> errors) => new ValidateResult
            { Status = ValidateStatus.Failed, ValidationErrors = errors };

    }

    enum ValidateStatus
    {
        Ok = 1,
        Failed = 2
    }
}
