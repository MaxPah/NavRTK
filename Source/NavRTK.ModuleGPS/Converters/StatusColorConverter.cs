using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    /// <summary>
    /// This is a converter for the statusbar at the bottom of the app
    /// if connection ok color will be Green
    /// else connection color will be Red
    /// </summary>
    public class StatusColorConverter : IValueConverter
    {
        public enum StatusEnum
        {
            ConnectionOK,
            ConnectionKO,
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = (string)value;

            if (state == StatusEnum.ConnectionOK.ToString())
                return new SolidColorBrush(Colors.Green);
            else return new SolidColorBrush(Colors.Red);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
