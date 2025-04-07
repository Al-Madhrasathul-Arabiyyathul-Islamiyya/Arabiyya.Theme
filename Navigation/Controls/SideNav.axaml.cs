﻿using Arabiyya.Theme.Navigation.Core.Services;
using Arabiyya.Theme.Navigation.Models;
using AvaGlass.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Arabiyya.Theme.Navigation.Controls;

/// <summary>
/// Interaction logic for SideNav.xaml.
/// Provides a sidebar navigation UI based on a NavigationConfig and INavigationService.
/// </summary>
public partial class SideNav : UserControl
{
    // Styled Properties for configuration and appearance
    public static readonly StyledProperty<bool> ShowIconsProperty =
        AvaloniaProperty.Register<SideNav, bool>(nameof(ShowIcons), true);

    public static readonly StyledProperty<bool> ShowLabelsProperty =
        AvaloniaProperty.Register<SideNav, bool>(nameof(ShowLabels), true);

    public static readonly StyledProperty<INavigationService?> NavigationServiceProperty =
        AvaloniaProperty.Register<SideNav, INavigationService?>(nameof(NavigationService));

    public static readonly StyledProperty<double> ExpandedWidthProperty =
        AvaloniaProperty.Register<SideNav, double>(nameof(ExpandedWidth), 220);

    public static readonly StyledProperty<double> CollapsedWidthProperty =
        AvaloniaProperty.Register<SideNav, double>(nameof(CollapsedWidth), 60);

    public static readonly StyledProperty<NavigationConfig?> ConfigProperty =
        AvaloniaProperty.Register<SideNav, NavigationConfig?>(nameof(Config));

    /// <summary>
    /// Gets or sets a value indicating whether icons should be displayed next to navigation items.
    /// </summary>
    public bool ShowIcons
    {
        get => GetValue(ShowIconsProperty);
        set => SetValue(ShowIconsProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether labels should be displayed next to navigation items.
    /// Automatically updated based on expanded state if AllowCollapse is true.
    /// </summary>
    public bool ShowLabels
    {
        get => GetValue(ShowLabelsProperty);
        set => SetValue(ShowLabelsProperty, value);
    }

    /// <summary>
    /// Gets or sets the navigation service instance used to control navigation state and actions.
    /// </summary>
    public INavigationService? NavigationService
    {
        get => GetValue(NavigationServiceProperty);
        set => SetValue(NavigationServiceProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the sidebar when it is fully expanded.
    /// </summary>
    public double ExpandedWidth
    {
        get => GetValue(ExpandedWidthProperty);
        set => SetValue(ExpandedWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the sidebar when it is collapsed (typically showing only icons).
    /// </summary>
    public double CollapsedWidth
    {
        get => GetValue(CollapsedWidthProperty);
        set => SetValue(CollapsedWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the navigation configuration containing items, header, footer, and settings.
    /// </summary>
    public NavigationConfig? Config
    {
        get => GetValue(ConfigProperty);
        set => SetValue(ConfigProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SideNav"/> class.
    /// </summary>
    public SideNav()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Sets up the control when it's attached to the visual tree.
    /// </summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        // Subscribe to theme changes
        if (Application.Current != null)
        {
            Application.Current.ActualThemeVariantChanged += OnApplicationThemeChanged;
        }

        // Set up Config property change monitoring
        if (Config != null)
        {
            Config.PropertyChanged += Config_PropertyChanged;
            UpdateWidth(Config.IsExpanded);
            UpdateVisualStates();
        }
    }

    /// <summary>
    /// Cleans up subscriptions when the control is detached from the visual tree.
    /// </summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        // Unsubscribe from theme changes
        if (Application.Current != null)
        {
            Application.Current.ActualThemeVariantChanged -= OnApplicationThemeChanged;
        }

        // Unsubscribe from Config changes
        if (Config != null)
        {
            Config.PropertyChanged -= Config_PropertyChanged;
        }
    }

    /// <summary>
    /// Handles property changes within the bound NavigationConfig object.
    /// </summary>
    private void Config_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(NavigationConfig.IsExpanded))
        {
            UpdateWidth(Config?.IsExpanded ?? true);
        }
        else if (e.PropertyName == nameof(NavigationConfig.UseGlassEffect))
        {
            UpdateVisualStates();
        }
    }

    /// <summary>
    /// Responds to application theme changes to update the glass/acrylic effect.
    /// </summary>
    private void OnApplicationThemeChanged(object? sender, EventArgs e)
    {
         UpdateVisualStates();
    }

    /// <summary>
    /// Updates visual states like glass/acrylic panel class based on current settings.
    /// </summary>
    private void UpdateVisualStates()
    {
        var glassPanel = this.FindControl<GlassmorphicPanel>("GlassPanel");
        if (glassPanel != null && Config != null)
        {
            UpdateGlassClass(glassPanel, Config.UseGlassEffect);
        }
    }

    /// <summary>
    /// Updates the width of the control and the visibility of labels based on the expanded state.
    /// </summary>
    /// <param name="isExpanded">True if the sidebar should be expanded, false otherwise.</param>
    private void UpdateWidth(bool isExpanded)
    {
        // Only set width and the ShowLabels property that styles depend on
        bool canCollapse = Config?.AllowCollapse ?? false;
        Width = isExpanded || !canCollapse ? ExpandedWidth : CollapsedWidth;
        ShowLabels = isExpanded;
        ShowIcons = true;
    }

    /// <summary>
    /// Sets the appropriate CSS class on the background panel based on the glass effect setting.
    /// </summary>
    private static void UpdateGlassClass(GlassmorphicPanel panel, bool useGlass)
    {
        panel.Classes.Set("GlassPanel", useGlass);
        panel.Classes.Set("AcrylicPanel", !useGlass);
    }
}
