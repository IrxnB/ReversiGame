using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ReversiLib;

namespace Reversi.Views
{
    
    [ValueConversion(typeof(bool), typeof(ImageSource))]
    internal class TurnImageSourceConverter : IValueConverter
    {
        private static string redCheckerUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\checkerRed.png";
        private static string blackCheckerUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\checkerBlack.png";
        private static string placeholderUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\placeholder.png";

        ImageSource redChecker = new BitmapImage(new Uri(redCheckerUri));
        ImageSource blackChecker = new BitmapImage(new Uri(blackCheckerUri));
        ImageSource placeholder = new BitmapImage(new Uri(placeholderUri));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? redChecker : blackChecker;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
