using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helpers
{
    public class MyCustomValidationAttributes : ValidationAttribute
    {
        public MyCustomValidationAttributes(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                string bookDescription = value.ToString();
                if(bookDescription.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage?? "Book Description does't contain desirrerd value");
        }
    }
}
