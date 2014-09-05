﻿using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NavRTK.ModuleGPS.Converters
{
    public class ColorConverter : IValueConverter
    {
        public enum StatusEnum
        {
            ConnectionOK,
            ConnectionKO,
            ConnectionLoading
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = (string)value;

            if (state == StatusEnum.ConnectionOK.ToString())
                return new SolidColorBrush(Colors.Green);
            else if (state == StatusEnum.ConnectionKO.ToString())
                return new SolidColorBrush(Colors.Red);
            else if (state == StatusEnum.ConnectionLoading.ToString())
                return new SolidColorBrush(Colors.Blue);
            else return new SolidColorBrush(Colors.Blue);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
