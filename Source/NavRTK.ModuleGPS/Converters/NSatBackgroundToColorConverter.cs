using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    class NSatBackgroundToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state;
            if (value != null)
                state = int.Parse(value.ToString());
            else state = 0;

            if (state >= 6)
                return new SolidColorBrush(Color.FromRgb(0, 67, 11)); // green dark
            else if (state == 5 )
                return new SolidColorBrush(Color.FromRgb(102, 66, 0)); // orange dark

            else return new SolidColorBrush(Color.FromRgb(91, 0, 0)); // red dark

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}