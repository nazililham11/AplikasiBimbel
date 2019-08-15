
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AplikasiBimbel.Converters
{
    public class LengthToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!(value is int))
                return Visibility.Collapsed;


            bool isNotEmpty = ((int)value) == 0;

            if (parameter == null)
                return (bool)isNotEmpty ? Visibility.Collapsed : Visibility.Visible;
            else
                return (bool)isNotEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
