using Arabiyya.Theme.Navigation.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Arabiyya.Theme.Navigation.Controls;

public partial class NavigationToggleButton : UserControl
{
    private Button? _button;

    public static readonly StyledProperty<IExpandable?> TargetProperty =
        AvaloniaProperty.Register<NavigationToggleButton, IExpandable?>(nameof(Target));

    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<NavigationToggleButton, bool>(nameof(IsExpanded));

    public IExpandable? Target
    {
        get => GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public NavigationToggleButton()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _button = this.FindControl<Button>("PART_Button");

        if (_button != null)
        {
            _button.Click += OnButtonClick;
        }
    }

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
                IsExpanded = newTarget.IsExpanded;
            }
        }
    }

    private void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        Target?.ToggleExpanded();
    }

    private void OnTargetExpansionChanged(object? sender, bool isExpanded)
    {
        IsExpanded = isExpanded;
    }
}
