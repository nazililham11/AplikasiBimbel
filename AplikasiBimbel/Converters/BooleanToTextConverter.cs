using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AplikasiBimbel.Converters
{
    public class BooleanToTextConverter : IValueConverter
	{

		public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
		{
			// Setting default values
			string valueIfTrue = "True";
			string valueIfFalse = "False";

			// Get the converter parameter
			if (parameter != null) {
				var parameterstring = parameter.ToString();

				if (!string.IsNullOrEmpty(parameterstring)) {
					string[] parameters = parameterstring.Split(';');

					if ((bool)values == true) { 
						//Get First Parameter
						if (parameters.Length > 0 && parameters[0] != null) 
							return parameters[0];

					} else {
						//Get Second Parameter
						if (parameters.Length > 1 && parameters[1] != null)
							return parameters[1];
					}
				}
			}

			if ((bool)values == true)
				return valueIfTrue;


			return valueIfFalse;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
