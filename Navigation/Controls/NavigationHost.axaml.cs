using Arabiyya.Theme.Navigation.Core.Events;
using Arabiyya.Theme.Navigation.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arabiyya.Theme.Navigation.Controls;

public partial class NavigationHost : UserControl
{
    private ContentControl? _contentPresenter;

    public static readonly StyledProperty<INavigator?> NavigatorProperty =
        AvaloniaProperty.Register<NavigationHost, INavigator?>(nameof(Navigator));

    public static readonly StyledProperty<string> TransitionTypeProperty =
        AvaloniaProperty.Register<NavigationHost, string>(nameof(TransitionType), "fade");

    public INavigator? Navigator
    {
        get => GetValue(NavigatorProperty);
        set => SetValue(NavigatorProperty, value);
    }

    public string TransitionType
    {
        get => GetValue(TransitionTypeProperty);
        set => SetValue(TransitionTypeProperty, value);
    }

    public NavigationHost()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _contentPresenter = this.FindControl<ContentControl>("PART_ContentPresenter");

        if (_contentPresenter != null)
        {
            _contentPresenter.Classes.Add(TransitionType);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == NavigatorProperty)
        {
            if (change.OldValue is INavigator oldNavigator)
            {
                oldNavigator.Navigated -= OnNavigatorNavigated;
            }

            if (change.NewValue is INavigator newNavigator)
            {
                newNavigator.Navigated += OnNavigatorNavigated;

                // Set initial content
                Content = newNavigator.SelectedItem?.Content;
            }
            else
            {
                Content = null;
            }
        }
        else if (change.Property == TransitionTypeProperty && _contentPresenter != null)
        {
            _contentPresenter.Classes.Clear();
            _contentPresenter.Classes.Add(TransitionType);
        }
    }

    private void OnNavigatorNavigated(object? sender, NavigatedEventArgs e)
    {
        Content = e.Content;

        // Apply transition
        if (_contentPresenter != null)
        {
            _contentPresenter.Classes.Clear();
            _contentPresenter.Classes.Add(TransitionType);
        }
    }
}
