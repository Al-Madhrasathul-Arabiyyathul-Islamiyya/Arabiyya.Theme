using Arabiyya.Theme.Navigation.Interfaces;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Arabiyya.Theme.Navigation.Controls;

/// <summary>
/// A button that toggles the expanded state of an IExpandable component
/// </summary>
public class NavigationToggleButton : ToggleButton
{
    /// <summary>
    /// Defines the Target dependency property
    /// </summary>
    public static readonly StyledProperty<IExpandable?> TargetProperty =
        AvaloniaProperty.Register<NavigationToggleButton, IExpandable?>(nameof(Target));

    /// <summary>
    /// Gets or sets the expandable target
    /// </summary>
    public IExpandable? Target
    {
        get => GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the NavigationToggleButton class
    /// </summary>
    public NavigationToggleButton()
    {
        this.Click += OnClick;

        // Add standard style class
        this.Classes.Add("navigation-toggle-button");
    }

    /// <summary>
    /// Called when properties change
    /// </summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == TargetProperty)
        {
            if (change.OldValue is IExpandable oldTarget)
            {
                oldTarget.ExpansionChanged -= OnTargetExpansionChanged;
            }

            if (change.NewValue is IExpandable newTarget)
            {
                newTarget.ExpansionChanged += OnTargetExpansionChanged;
                IsChecked = newTarget.IsExpanded;
            }
        }
    }

    /// <summary>
    /// Called when the button is clicked
    /// </summary>
    private void OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Target?.ToggleExpanded();
    }

    /// <summary>
    /// Called when the target's expansion state changes
    /// </summary>
    private void OnTargetExpansionChanged(object? sender, bool isExpanded)
    {
        IsChecked = isExpanded;
    }
}
