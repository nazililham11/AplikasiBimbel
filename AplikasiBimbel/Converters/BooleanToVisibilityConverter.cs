using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AplikasiBimbel.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			if (!(value is bool))
				return Visibility.Collapsed;


			if (parameter == null)
				return (bool)value ? Visibility.Visible : Visibility.Collapsed;
			else
				return (bool)value ? Visibility.Collapsed : Visibility.Visible;


		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
				 
	}
}
