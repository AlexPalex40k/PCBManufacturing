using Microsoft.Extensions.DependencyInjection;
using PCBManufacturing.Features.Order;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using PCBManufacturing.ViewModels;
using PCBManufacturing.Views;
using System.Windows;

namespace PCBManufacturing;

public partial class App : Application
{
    private readonly Mutex _namedMutex;
    private readonly bool _isFirstInstance;

    private readonly ServiceProvider? _serviceProvider;

    public App()
    {
        var mutexName = BuildMutexName(Environment.UserName);
        _namedMutex = new Mutex(true, mutexName, out _isFirstInstance);

        if (_isFirstInstance)
        {
            _serviceProvider = Module.CreateServiceProvider();

            var configurationService = _serviceProvider.GetRequiredService<IPcbConfigurationService>();
            configurationService.Load();
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (!_isFirstInstance)
        {
            Shutdown();
            return;
        }

        var mainWindow = _serviceProvider!.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_isFirstInstance)
        {
            var serviceProvider = _serviceProvider!;

            var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();

            mainWindowViewModel.SaveState();

            _namedMutex.ReleaseMutex();
            serviceProvider.Dispose();
        }

        _namedMutex.Dispose();

        base.OnExit(e);
    }

    private static string BuildMutexName(string userName)
    {
        var applicationName =
            typeof(App).Assembly.GetName().Name
            ?? "PCBManufacturing";

        var mutexName = $"{userName}-{applicationName}";

        mutexName = mutexName.Replace('\\', '_');
        mutexName = mutexName.Replace('.', '_');
        mutexName = mutexName.Replace(' ', '_');

        return $"Global\\{mutexName}";
    }
}