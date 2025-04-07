using Arabiyya.Theme.Navigation.Core.Models;
using Arabiyya.Theme.Navigation.Core.Services;
using Arabiyya.Theme.Navigation.Models;

namespace Arabiyya.Theme.Navigation.Interfaces;

/// <summary>
/// Interface for components that can navigate between items
/// </summary>
public interface INavigator
{
    /// <summary>
    /// Gets the currently selected navigation item
    /// </summary>
    NavigationItem? SelectedItem { get; }

    /// <summary>
    /// Gets the collection of available navigation items
    /// </summary>
    IReadOnlyList<NavigationItem>? Items { get; }

    /// <summary>
    /// Navigates to the specified item
    /// </summary>
    Task<bool> NavigateToAsync(NavigationItem item);

    /// <summary>
    /// Navigates to the item with the specified ID
    /// </summary>
    Task<bool> NavigateToAsync(string itemId);

    /// <summary>
    /// Event raised when navigation occurs
    /// </summary>
    event EventHandler<NavigatedEventArgs>? Navigated;
}
