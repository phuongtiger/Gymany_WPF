using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyWPF.Helper
{
    // MinLengthValidationRule
    public class MinLengthValidationRule : ValidationRule
    {
        public int MinLength { get; set; } = 8;  // Đặt giá trị mặc định là 8 ký tự

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length < MinLength)
            {
                return new ValidationResult(false, $"Must be at least {MinLength} characters.");
            }
            return ValidationResult.ValidResult;
        }
    }

    // EmailValidationRule
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !Regex.IsMatch(value.ToString(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ValidationResult(false, "Invalid email format.");
            }
            return ValidationResult.ValidResult;
        }
    }

    // PhoneValidationRule
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !Regex.IsMatch(value.ToString(), @"^\d{10}$"))  // Đảm bảo 10 chữ số
            {
                return new ValidationResult(false, "Invalid phone number format.");
            }
            return ValidationResult.ValidResult;
        }
    }

  
}
