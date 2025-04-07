using Arabiyya.Theme.Navigation.Core.Models;

using Arabiyya.Theme.Navigation.Core.Services;

namespace Arabiyya.Theme.Navigation.Core.Events;


/// <summary>
/// Message sent when navigation has completed
/// </summary>
public record NavigationCompletedMessage(NavigationItem Item, object Content);

/// <summary>
/// Message sent when a navigation guard rejects navigation
/// </summary>
public record NavigationGuardRejectedMessage(
    NavigationItem SourceItem,
    NavigationItem TargetItem,
    NavigationGuardResult Result);
