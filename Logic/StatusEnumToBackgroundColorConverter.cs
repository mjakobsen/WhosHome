using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WhosHome.Logic
{
    public class StatusEnumToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StatusEnum)) return null;

            Brushes returnColor;

            switch ((StatusEnum)value)
            {
                case StatusEnum.Free:
                    return Brushes.Blue;
                    break;
                case StatusEnum.Busy:
                    return Brushes.Red;
                    break;
                case StatusEnum.Home:
                    return Brushes.Green;
                    break;
                case StatusEnum.Service:
                    return Brushes.Yellow;
                    break;
                case StatusEnum.OutOfService:
                    return Brushes.Orange;
                    break;
                default:
                    return Brushes.Green;
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