using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.Features.Preferences;
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

        return services.BuildServiceProvider();
    }
}