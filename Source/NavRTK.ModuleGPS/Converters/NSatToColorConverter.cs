using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    class NSatToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state = int.Parse(value.ToString());

            if (state >= 6)
                return new SolidColorBrush(Color.FromRgb(0, 135, 22)); // green light
            else if (state == 5 )
                return new SolidColorBrush(Color.FromRgb(255, 165, 0)); // orange light

            else return new SolidColorBrush(Color.FromRgb(229, 0, 0)); // red light

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
