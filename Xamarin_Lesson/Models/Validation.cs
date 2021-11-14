using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Lesson.Models
{
    abstract class Validation
    {
        public abstract ValidateResult Validate();
    }

    internal abstract class Validation<T> : Validation
    {
        protected T model;

        protected Validation(T model)
        {
            this.model = model;
        }
    }
}
