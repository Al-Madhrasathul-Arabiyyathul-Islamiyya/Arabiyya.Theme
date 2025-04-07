using System.Collections.ObjectModel;
using System.ComponentModel;
using Arabiyya.Theme.Navigation.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Arabiyya.Theme.Navigation.Core.Models;

/// <summary>
/// Defines configuration options for the navigation system
/// </summary>
public partial class NavigationConfig : ObservableObject, IExpandable
{
    /// <summary>
    /// Gets or sets the navigation mode
    /// </summary>
    [ObservableProperty]
    private NavigationMode _navigationMode = NavigationMode.Sidebar;

    /// <summary>
    /// Gets or sets the sidebar position
    /// </summary>
    [ObservableProperty]
    private SidebarPosition _sidebarPosition = SidebarPosition.Left;

    /// <summary>
    /// Gets or sets the navigation items
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<NavigationItem> _items = [];

    /// <summary>
    /// Gets or sets the header content (optional)
    /// </summary>
    [ObservableProperty]
    private object? _headerContent;

    /// <summary>
    /// Gets or sets the footer content (optional)
    /// </summary>
    [ObservableProperty]
    private object? _footerContent;

    /// <summary>
    /// Gets or sets the title displayed in the navigation panel
    /// </summary>
    [ObservableProperty]
    private string? _title;

    /// <summary>
    /// Gets or sets a value indicating whether the navigation panel is expanded
    /// </summary>
    [ObservableProperty]
    private bool _isExpanded = true;

    /// <summary>
    /// Gets or sets a value indicating whether to show icons
    /// </summary>
    [ObservableProperty]
    private bool _showIcons = true;

    /// <summary>
    /// Gets or sets a value indicating whether to show labels
    /// </summary>
    [ObservableProperty]
    private bool _showLabels = true;

    /// <summary>
    /// Gets or sets the width of the sidebar when expanded
    /// </summary>
    [ObservableProperty]
    private double _expandedWidth = 220;

    /// <summary>
    /// Gets or sets whether the sidebar can be collapsed
    /// </summary>
    [ObservableProperty]
    private bool _allowCollapse = true;

    /// <summary>
    /// Gets or sets the width of the sidebar when collapsed (icon-only mode)
    /// </summary>
    [ObservableProperty]
    private double _collapsedWidth = 60;

    /// <summary>
    /// Gets or sets a value indicating whether to use glass effect
    /// </summary>
    [ObservableProperty]
    private bool _useGlassEffect = true;

    /// <summary>
    /// Gets or sets the currently selected navigation item ID
    /// </summary>
    [ObservableProperty]
    private string? _selectedItemId;

    /// <summary>
    /// Gets or sets the default layout name
    /// </summary>
    [ObservableProperty]
    private string _defaultLayout = "default";

    [RelayCommand]
    public void ToggleExpanded()
    {
        IsExpanded = !IsExpanded;
        ExpansionChanged?.Invoke(this, IsExpanded);
    }

    /// <summary>
    /// Event raised when the expanded state changes
    /// </summary>
    public event EventHandler<bool>? ExpansionChanged;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(IsExpanded))
        {
            ExpansionChanged?.Invoke(this, IsExpanded);
        }
    }

    /// <summary>
    /// Serializes the configuration to JSON
    /// </summary>
    public string ToJson()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Deserializes configuration from JSON
    /// </summary>
    public static NavigationConfig FromJson(string json)
    {
        var result = System.Text.Json.JsonSerializer.Deserialize<NavigationConfig>(json);
        return result ?? throw new InvalidOperationException("Deserialization returned null.");
    }
}
