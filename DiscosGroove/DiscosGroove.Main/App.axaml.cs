using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DiscosGroove.Main.DependencyInjection;
using DiscosGroove.Main.ViewModels;
using DiscosGroove.Main.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DiscosGroove.Main;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceProvider provider = Bootstrapper.Up();
            
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindow mainWindow = provider.GetRequiredService<MainWindow>();
            MainWindowViewModel mainWindowViewModel = provider.GetRequiredService<MainWindowViewModel>();
                
            mainWindow.DataContext = mainWindowViewModel;
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}