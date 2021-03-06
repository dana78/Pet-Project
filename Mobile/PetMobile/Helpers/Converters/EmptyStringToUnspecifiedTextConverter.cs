﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace PetMobile.Helpers.Converters
{
    public class EmptyStringToUnspecifiedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value?.ToString();
            if (string.IsNullOrEmpty(text))
            {
                text = "No especificado";
            }
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
