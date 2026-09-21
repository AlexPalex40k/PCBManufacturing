using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;
using PCBManufacturing.ViewModels;

namespace PCBManufacturing;

public static class Module
{
    public static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<PreferencesViewModel>();
        services.AddSingleton<QuoteViewModel>();

        return services.BuildServiceProvider();
    }
}