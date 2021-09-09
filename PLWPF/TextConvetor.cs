using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.IO;

namespace PLWPF
{
    class TextConvetor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //if (!File.Exists((string)value))
                //    throw new Exception("");

                //BitmapImage b = new BitmapImage(new Uri((string)value, UriKind.RelativeOrAbsolute));
                //Console.WriteLine(b.DpiX);
                //return b;
                return value.ToString();
            }
            catch (Exception ex)
            {
                return "";
                //return new BitmapImage(new Uri(@"C:\Users\moriy\Downloads\Telegram Desktop\16-01-19\PLWPF\pictures\passPort.jpg", UriKind.RelativeOrAbsolute));
            }
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return "";
            }
            catch
            {
                return "";
            }
        }


    }
}
