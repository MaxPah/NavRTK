using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    /// <summary>
    /// This is a converter for SerialPort color
    /// if Id = 0 Color will be White
    /// else according Id color will be different blue
    /// </summary>
    class NamePortColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state;
            if(value != null)
                state = int.Parse(value.ToString());
            else state = 1;

            if (state == 0)              
                return new SolidColorBrush(Colors.White);
            else if (state %3 == 0)
                return new SolidColorBrush(Color.FromRgb(81, 142, 239)); //secondColor #2672ec
            else if (state % 3 == 1)
                return new SolidColorBrush(Color.FromRgb(38, 114, 236)); //bleu
            else
                return new SolidColorBrush(Color.FromRgb(30,91,188)); //bleu

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}