using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AplikasiBimbel.Converters
{
    public class VisibilityNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return Visibility.Collapsed;


            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
