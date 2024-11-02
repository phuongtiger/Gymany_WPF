using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MyWPF.Helper
{
    //public class EmailValidationRule : ValidationRule
    //{
    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        var email = value as string;
    //        if (string.IsNullOrEmpty(email))
    //        {
    //            return new ValidationResult(false, "Email is required.");
    //        }

    //        // Email format validation pattern
    //        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    //        if (!Regex.IsMatch(email, emailPattern))
    //        {
    //            return new ValidationResult(false, "Email address is not valid.");
    //        }

    //        return ValidationResult.ValidResult;
    //    }
    //}
}
