using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    /// <summary>
    /// This is a converter to determine the default SerialPort
    /// If Id = 0 text will be "default"
    /// Else text will be ""
    /// </summary>
    public class DefaultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state;
            if (value != null)
                state = int.Parse(value.ToString());
            else state = 1;

            if (state == 0)
                return "Default";
            else return String.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}