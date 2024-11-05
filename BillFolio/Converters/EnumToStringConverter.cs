using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace BillFolio.Converters
{
	public class EnumToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Ensure value is not null
			if (value == null)
				return string.Empty;

			// Return the string representation of the enum value
			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;

			// Get the enum type from the target type
			var enumType = targetType;
			if (!enumType.IsEnum)
				throw new InvalidOperationException("Target type must be an Enum");

			return Enum.Parse(enumType, value.ToString()); //Converts string bak to enum
		}
	}
}
