using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Helpers
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {

        public string Text { get; set; }

        public MyCustomValidationAttribute(string text)
        {
            Text = text;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "The Book does not contain the required value");
        }
    }
}
