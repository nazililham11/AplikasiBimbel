using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AplikasiBimbel.Converters
{
	public class TextToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			if (!(value is string))
				return Visibility.Collapsed;


			bool isNotEmpty = !string.IsNullOrEmpty(value as string);

			if (parameter == null)
				return (bool) isNotEmpty ? Visibility.Collapsed : Visibility.Visible;
			else
				return (bool) isNotEmpty ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

	}

}
