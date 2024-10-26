using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using MyWPF.ViewModel;

public class ImageHelper : BaseViewModel
{
    private BitmapImage _imageSource;
    public BitmapImage ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged(nameof(ImageSource)); // Thông báo thay đổi để cập nhật UI
        }
    }

    public string BrowseImage()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.jpg, *.png)|*.jpg;*.png"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFilePath = openFileDialog.FileName;

            // Tạo BitmapImage từ đường dẫn và gán vào ImageSource
            ImageSource = LoadImage(selectedFilePath);

            return selectedFilePath; // Trả về đường dẫn ảnh
        }

        return null; // Trả về null nếu người dùng không chọn file nào
    }

   
    public static BitmapImage LoadImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
        {
            return null; // Nếu đường dẫn không tồn tại hoặc rỗng
        }

        try
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
