﻿using System.Globalization;
using Avalonia.Data.Converters;

namespace Arabiyya.Theme.Converters;

/// <summary>
/// Converts a boolean expanded state to an icon character
/// </summary>
public class ExpandIconConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean to an icon
    /// </summary>
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture) => value is bool expanded && expanded
            ? "\uE1CE" // Collapse icon (left arrow)
            : "\uE1D0"; // Expand icon (right arrow)

    /// <summary>
    /// Not implemented
    /// </summary>
    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
