using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.Features.Order;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using PCBManufacturing.ViewModels;
using PCBManufacturing.Views;

namespace PCBManufacturing;

public static class Module
{
    public static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<PcbConfiguration>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IPostcodeValidator, PostcodeValidator>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<PreferencesViewModel>();
        services.AddSingleton<QuoteViewModel>();
        services.AddSingleton<OrderViewModel>();

        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IConfigurationStorage, JsonConfigurationStorage>();

        return services.BuildServiceProvider();
    }
}