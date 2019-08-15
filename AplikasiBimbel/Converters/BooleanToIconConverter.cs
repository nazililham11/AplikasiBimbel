using System;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace AplikasiBimbel.Converters
{
	public class BooleanToIconConverter : IValueConverter
	{

		public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
		{
			// Setting default values
			PackIconKind iconIfTrue = PackIconKind.Circle;
			PackIconKind iconIfFalse = PackIconKind.Close;

			// Get the converter parameter
			if (parameter != null) {
				var parameterstring = parameter.ToString();

				if (!string.IsNullOrEmpty(parameterstring)) {
					string[] parameters = parameterstring.Split(';');

					//Get First Parameter
					if (parameters.Length > 0 && !string.IsNullOrEmpty(parameters[0])) {

						if (int.TryParse(parameters[0], out int valueIfTrue))
							iconIfTrue = (PackIconKind)valueIfTrue;
					}

					//Get Second Parameter
					if (parameters.Length > 1 && !string.IsNullOrEmpty(parameters[1])) {

						if (int.TryParse(parameters[1], out int valueIfFalse))
							iconIfFalse = (PackIconKind)valueIfFalse;
					}
				}
			}

			// Creating Color Brush
			if ((bool)values == true)
				return iconIfTrue;


			return iconIfFalse;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	
}
