using Arabiyya.Theme.Navigation.Core.Models;

namespace Arabiyya.Theme.Navigation.Core.Events;

/// <summary>
/// Event arguments for the Navigating event
/// </summary>
public class NavigatingEventArgs : EventArgs
{
    /// <summary>
    /// Gets the source navigation item
    /// </summary>
    public NavigationItem SourceItem { get; }

    /// <summary>
    /// Gets the target navigation item
    /// </summary>
    public NavigationItem TargetItem { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the navigation should be canceled
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// Creates a new instance of NavigatingEventArgs
    /// </summary>
    /// <param name="sourceItem">The source navigation item</param>
    /// <param name="targetItem">The target navigation item</param>
    public NavigatingEventArgs(NavigationItem sourceItem, NavigationItem targetItem)
    {
        SourceItem = sourceItem;
        TargetItem = targetItem;
    }
}

/// <summary>
/// Event arguments for the Navigated event
/// </summary>
public class NavigatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the navigation item that was navigated to
    /// </summary>
    public NavigationItem Item { get; }

    /// <summary>
    /// Gets the content that was navigated to
    /// </summary>
    public object Content { get; }

    /// <summary>
    /// Creates a new instance of NavigatedEventArgs
    /// </summary>
    /// <param name="item">The navigation item that was navigated to</param>
    /// <param name="content">The content that was navigated to</param>
    public NavigatedEventArgs(NavigationItem item, object content)
    {
        Item = item;
        Content = content;
    }
}
