using ReversiLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Reversi.Views
{
    [ValueConversion(typeof(CellState), typeof(ImageSource))]
    internal class StateImageConverter : IValueConverter
    {
        private static string redCheckerUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\checkerRed.png";
        private static string blackCheckerUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\checkerBlack.png";
        private static string placeholderUri = "C:\\Users\\Андрей Лузгин\\source\\repos\\ReversiGame\\ReversiGame\\Resources\\placeholder.png";

        ImageSource redChecker = new BitmapImage(new Uri(redCheckerUri));
        ImageSource blackChecker = new BitmapImage(new Uri(blackCheckerUri));
        ImageSource placeholder = new BitmapImage(new Uri(placeholderUri));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CellState)value)
            {
                case CellState.SecondPlayer:
                    return blackChecker;
                case CellState.FirstPlayer:
                    return redChecker;
                default:
                    return placeholder;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return CellState.None;
        }
    }
}
