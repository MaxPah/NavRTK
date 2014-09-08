using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    public class DefaultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = value.ToString();

            if (state == "0")
                return new SolidColorBrush(Colors.White); //White
            else return new SolidColorBrush(Colors.Transparent); //#2672EC = secondColor

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}