using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyWPF.Helper
{
    public class OrderStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Kiểm tra xem OrderStatus có phải là "Waiting" không
            if (value is string orderStatus && orderStatus == "Waiting")
            {
                return Visibility.Visible; // Hiển thị nút nếu trạng thái là "Waiting"
            }
            return Visibility.Collapsed; // Ẩn nút nếu trạng thái không phải là "Waiting"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không cần xử lý ConvertBack trong trường hợp này
            throw new NotImplementedException();
        }
    }
}
