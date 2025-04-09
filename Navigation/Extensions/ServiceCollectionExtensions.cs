using Arabiyya.Theme.Navigation.Core.Services;
using Arabiyya.Theme.Navigation.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Arabiyya.Theme.Navigation.Extensions;

/// <summary>
/// Extension methods for registering navigation services with DI
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds navigation services to the service collection
    /// </summary>
    public static IServiceCollection AddNavigation(this IServiceCollection services)
    {
        // Register the messenger
        services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

        // Register the default view factory
        services.AddSingleton<IViewFactory, DefaultViewFactory>();

        // Register the navigation service (also implements INavigator)
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<INavigator>(sp => (INavigator)sp.GetRequiredService<INavigationService>());

        return services;
    }

    /// <summary>
    /// Adds navigation services with DI support for view resolution
    /// </summary>
    public static IServiceCollection AddNavigationWithDI(this IServiceCollection services)
    {
        // Register the messenger
        services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

        // Register the DI view factory
        services.AddSingleton<IViewFactory>(sp => new DependencyInjectionViewFactory(sp));

        // Register the navigation service (also implements INavigator)
        services.AddSingleton<INavigationService>(sp =>
        {
            var viewFactory = sp.GetRequiredService<IViewFactory>();
            var messenger = sp.GetRequiredService<IMessenger>();

            return new NavigationService(viewFactory, messenger);
        });

        services.AddSingleton<INavigator>(sp => (INavigator)sp.GetRequiredService<INavigationService>());

        return services;
    }
}
