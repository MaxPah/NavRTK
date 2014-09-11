using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    class NamePortColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state = int.Parse(value.ToString());

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