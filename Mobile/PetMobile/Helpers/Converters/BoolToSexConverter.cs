using System;
using System.Globalization;
using Xamarin.Forms;

namespace PetMobile.Helpers.Converters
{
    public class BoolToSexConverter : IValueConverter
    {
        private const string Male = "Macho";
        private const string Female = "Hembra";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sexBoolValue = (bool?)value;
            var sexStringValue = "No especificado";
            if (sexBoolValue.HasValue)
            {
                sexStringValue = sexBoolValue.Value ? Male : Female;
            }
            return sexStringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sexStringValue = value.ToString();
            if (sexStringValue.Equals(Male))
            {
                return true;
            }
            else if (sexStringValue.Equals(Female))
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
