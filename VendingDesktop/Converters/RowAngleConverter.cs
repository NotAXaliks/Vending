using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace VendingDesktop.Converters;

public class ArrowAngleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? 180.0 : 0.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}