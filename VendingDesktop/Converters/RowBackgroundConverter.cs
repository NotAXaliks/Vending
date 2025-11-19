using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace VendingDesktop.Converters;

public class RowBackgroundConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index) return SolidColorBrush.Parse(index % 2 == 0 ? "#e6e6e6" : "#FFFFFF");

        return SolidColorBrush.Parse("#FFFFFF");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}