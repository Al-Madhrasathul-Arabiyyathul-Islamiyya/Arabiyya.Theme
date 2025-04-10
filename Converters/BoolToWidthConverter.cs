using Avalonia.Data.Converters;

namespace Arabiyya.Theme.Converters;

public class BoolToWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not bool isExpanded || parameter is not string widthValues)
        {
            return 220;
        }

        string[] values = widthValues.Split(',');
        if (values.Length < 2)
        {
            return 220;
        }

        return isExpanded switch
        {
            true when double.TryParse(values[0], out double expandedWidth) => expandedWidth,
            false when double.TryParse(values[1], out double collapsedWidth) => collapsedWidth,
            _ => 220
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
