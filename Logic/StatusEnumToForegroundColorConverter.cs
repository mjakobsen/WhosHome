using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WhosHome.Logic
{
    public class StatusEnumToForegroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StatusEnum)) return null;

            Brushes returnColor;

            switch ((StatusEnum)value)
            {
                case StatusEnum.Free:
                    return Brushes.White;
                    break;
                case StatusEnum.Busy:
                    return Brushes.White;
                    break;
                case StatusEnum.Home:
                    return Brushes.White;
                    break;
                case StatusEnum.Service:
                    return Brushes.Black;
                    break;
                case StatusEnum.OutOfService:
                    return Brushes.Black;
                    break;
                default:
                    return Brushes.White;
                    break;
            }

            return returnColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}