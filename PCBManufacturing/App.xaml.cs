using System.Windows;
using Microsoft.Extensions.DependencyInjection;

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
        _serviceProvider.Dispose();

        base.OnExit(e);
    }
}