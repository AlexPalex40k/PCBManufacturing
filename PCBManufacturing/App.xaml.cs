using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.Services;
using PCBManufacturing.ViewModels;
using System.Windows;
using PCBManufacturing.Views;

namespace PCBManufacturing;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = Module.CreateServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

        mainWindowViewModel.SaveState();

        _serviceProvider.Dispose();

        base.OnExit(e);
    }
}