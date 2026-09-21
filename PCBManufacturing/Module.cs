using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.ViewModels;

namespace PCBManufacturing;

public static class Module
{
    public static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }
}