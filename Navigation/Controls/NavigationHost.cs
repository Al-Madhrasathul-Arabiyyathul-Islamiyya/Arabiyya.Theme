using Arabiyya.Theme.Navigation.Core.Services;
using Arabiyya.Theme.Navigation.Interfaces;
using Avalonia;
using Avalonia.Controls;

namespace Arabiyya.Theme.Navigation.Controls;

/// <summary>
/// A content control that displays the current content of a navigation service
/// </summary>
public partial class NavigationHost : ContentControl
{
    /// <summary>
    /// Defines the Navigator dependency property
    /// </summary>
    public static readonly StyledProperty<INavigator?> NavigatorProperty =
        AvaloniaProperty.Register<NavigationHost, INavigator?>(nameof(Navigator));

    /// <summary>
    /// Defines the TransitionType dependency property
    /// </summary>
    public static readonly StyledProperty<string> TransitionTypeProperty =
        AvaloniaProperty.Register<NavigationHost, string>(nameof(TransitionType), "fade");

    /// <summary>
    /// Gets or sets the navigator
    /// </summary>
    public INavigator? Navigator
    {
        get => GetValue(NavigatorProperty);
        set => SetValue(NavigatorProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of transition to use
    /// </summary>
    public string TransitionType
    {
        get => GetValue(TransitionTypeProperty);
        set => SetValue(TransitionTypeProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the NavigationHost class
    /// </summary>
    public NavigationHost()
    {
        // Apply the navigation-host style class
        Classes.Add("navigation-host");
    }

    /// <summary>
    /// Called when properties change
    /// </summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == NavigatorProperty)
        {
            // Unsubscribe from old navigator
            if (change.OldValue is INavigator oldNavigator)
            {
                oldNavigator.Navigated -= OnNavigated;
            }

            // Subscribe to new navigator
            if (change.NewValue is INavigator newNavigator)
            {
                newNavigator.Navigated += OnNavigated;

                // Set initial content
                Content = newNavigator.SelectedItem?.Content;
            }
        }
        else if (change.Property == TransitionTypeProperty)
        {
            UpdateTransition();
        }
    }

    /// <summary>
    /// Called when the navigator navigates
    /// </summary>
    private void OnNavigated(object? sender, NavigatedEventArgs e)
    {
        Content = e.Content;
        UpdateTransition();
    }

    /// <summary>
    /// Updates the transition effect
    /// </summary>
    private void UpdateTransition()
    {
        var presenter = this.FindControl<ContentControl>("ContentPresenter");
        if (presenter != null)
        {
            presenter.Classes.Clear();
            presenter.Classes.Add(TransitionType);
        }
    }
}
