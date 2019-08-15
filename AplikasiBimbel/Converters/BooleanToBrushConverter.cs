using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AplikasiBimbel.Converters
{
	public class BooleanToBrushConverter : IValueConverter
	{
		#region Implementation of IValueConverter

		/// <summary>
		/// Converts Booleam Input To Solid Color Brush
		/// </summary>
		/// <param name="parameter">A CSV string on the format [ColorNameIfTrue;ColorNameIfFalse;OpacityNumber] may be provided for customization, default is [LimeGreen;Transperent;1.0].</param>
		/// <returns>A SolidColorBrush in the supplied or default colors depending on the state of value.</returns>


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Setting default values
			var colorIfTrue = Colors.Blue;
			var colorIfFalse = Colors.Red;

			// Parsing converter parameter
			// Parameter format: [ColorNameIfTrue;ColorNameIfFalse;]
			if (parameter != null) {
				var parameterstring = parameter.ToString();

				if (!string.IsNullOrEmpty(parameterstring)) {
					string[] parameters = parameterstring.Split(';');
					int count = parameters.Length;

					if (count > 0 && !string.IsNullOrEmpty(parameters[0])) {
						if (parameters[0].Contains("#")) {
							colorIfTrue = ColorFromHex(parameters[0]);
						} else
							colorIfTrue = ColorFromName(parameters[0]);
					}

					if (count > 1 && !string.IsNullOrEmpty(parameters[1])) {
						if (parameters[1].Contains("#")) {
							colorIfFalse = ColorFromHex(parameters[1]);
						} else
							colorIfFalse = ColorFromName(parameters[1]);
					}
				}
			}

			// Creating Color Brush
			if ((bool)value)
				return new SolidColorBrush(colorIfTrue);


			return new SolidColorBrush(colorIfFalse);
		}


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		public static Color ColorFromName(string colorName)
		{
			System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
			return Color.FromArgb(systemColor.A, systemColor.R, systemColor.G, systemColor.B);
		}

		public static Color ColorFromHex(string colorHex)
        { 
            return (Color)new BrushConverter().ConvertFrom(colorHex);
        }
	}
}
