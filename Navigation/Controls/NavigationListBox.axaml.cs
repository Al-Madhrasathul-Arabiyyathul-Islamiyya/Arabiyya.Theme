using Arabiyya.Theme.Navigation.Core.Events;
using Arabiyya.Theme.Navigation.Core.Models;
using Arabiyya.Theme.Navigation.Core.Services;
using Arabiyya.Theme.Navigation.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arabiyya.Theme.Navigation.Controls;

public partial class NavigationListBox : UserControl
{
    private ListBox? _listBox;

    public static readonly StyledProperty<INavigator?> NavigatorProperty =
        AvaloniaProperty.Register<NavigationListBox, INavigator?>(nameof(Navigator));

    public static readonly StyledProperty<bool> ShowIconsProperty =
        AvaloniaProperty.Register<NavigationListBox, bool>(nameof(ShowIcons), true);

    public static readonly StyledProperty<bool> ShowLabelsProperty =
        AvaloniaProperty.Register<NavigationListBox, bool>(nameof(ShowLabels), true);

    public INavigator? Navigator
    {
        get => GetValue(NavigatorProperty);
        set => SetValue(NavigatorProperty, value);
    }

    public bool ShowIcons
    {
        get => GetValue(ShowIconsProperty);
        set => SetValue(ShowIconsProperty, value);
    }

    public bool ShowLabels
    {
        get => GetValue(ShowLabelsProperty);
        set => SetValue(ShowLabelsProperty, value);
    }

    public NavigationListBox()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _listBox = this.FindControl<ListBox>("PART_ListBox");

        if (_listBox != null)
        {
            _listBox.SelectionChanged += OnSelectionChanged;
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

            if (change.NewValue is INavigator newNavigator && _listBox != null)
            {
                _listBox.ItemsSource = newNavigator.Items;
                System.Diagnostics.Debug.WriteLine($"Navigator set. ItemsSource is now set: {_listBox.ItemsSource != null}, Item Count: {newNavigator.Items?.Count ?? 0}");

                newNavigator.Navigated += OnNavigatorNavigated;

                if (newNavigator.SelectedItem != null && _listBox.ItemsSource != null)
                {
                    // Check if the item actually exists in the source before selecting
                    if (newNavigator.Items?.Contains(newNavigator.SelectedItem) == true)
                    {
                        _listBox.SelectedItem = newNavigator.SelectedItem;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Warning: Initial SelectedItem {newNavigator.SelectedItem.Id} not found in ItemsSource.");
                    }
                }
            }
            else if (_listBox != null)
            {
                _listBox.ItemsSource = null;
                System.Diagnostics.Debug.WriteLine($"Navigator set to null. ItemsSource cleared.");
            }
        }
        else if (change.Property == ShowLabelsProperty)
        {
            // We may want to adjust layout based on whether labels are shown
            this.InvalidateVisual();
        }
    }

    private void OnNavigatorNavigated(object? sender, NavigatedEventArgs e)
    {
        // Update selection when navigation occurs
        if (e.Item != null && _listBox != null && !Equals(_listBox.SelectedItem, e.Item))
        {
            _listBox.SelectedItem = e.Item;
        }
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (Navigator != null && e.AddedItems.Count > 0 && e.AddedItems[0] is NavigationItem item)
        {
            // Navigate to the selected item
            _ = Navigator.NavigateToAsync(item);
        }
    }
}
