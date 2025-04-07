namespace Arabiyya.Theme.Navigation.Interfaces;

/// <summary>
/// Interface for components that can be expanded or collapsed
/// </summary>
public interface IExpandable
{
    /// <summary>
    /// Gets or sets whether the component is expanded
    /// </summary>
    bool IsExpanded { get; set; }

    /// <summary>
    /// Toggles the expanded state
    /// </summary>
    void ToggleExpanded();

    /// <summary>
    /// Event raised when expansion state changes
    /// </summary>
    event EventHandler<bool>? ExpansionChanged;
}
