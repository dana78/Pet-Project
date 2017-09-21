using System;
using System.Globalization;
using Xamarin.Forms;

namespace PetMobile.Helpers.Converters
{
    public class CountToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value) > 0;
        }
    }
}
