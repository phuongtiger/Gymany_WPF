
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MyWPF.Helper
{
    //public class PhoneValidationRule : ValidationRule
    //{
    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        var phone = value as string;
    //        if (string.IsNullOrEmpty(phone))
    //        {
    //            return new ValidationResult(false, "Phone number is required.");
    //        }

    //        // Kiểm tra định dạng số điện thoại
    //        var phonePattern = @"^[0-9]{10}$"; // Ví dụ: 10 chữ số
    //        if (!Regex.IsMatch(phone, phonePattern))
    //        {
    //            return new ValidationResult(false, "Phone number is not valid. It should be 10 digits.");
    //        }

    //        return ValidationResult.ValidResult;
    //    }
    //}

}
