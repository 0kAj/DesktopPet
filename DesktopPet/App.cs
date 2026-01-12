using System.Windows;
using DesktopPet.Background;
using DesktopPet.Data.Pet;
using DesktopPet.Factory;
using DesktopPet.MiniGames;
using DesktopPet.WPF;
using DesktopPet.WPF.WindowViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesktopPet;

public class App : Application
{
    private static IHost? _host;
    public static IHost Host => _host ??= CreateHostBuilder().Build();

    [STAThread]
    public static void Main()
    {
        _host = CreateHostBuilder().Build();
        _host.Start();
        
        var app = new App();
        app.Startup += Application_Startup;
        app.Run();
        
        _host.StopAsync().GetAwaiter().GetResult();
        _host.Dispose();
    }

    private static HostBuilder CreateHostBuilder()
    {
        var builder = new HostBuilder();
        builder.ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<PetStatUpdater>();
            services.AddHostedService(provider => provider.GetRequiredService<PetStatUpdater>());
            
            services.AddTransient<WelcomeWindow>();
            services.AddTransient<WelcomeWindowViewModel>();

            services.AddSingleton<PetEventManager>();
            services.AddSingleton<GameManager>();

            services.AddTransient<RewardsWindow>();
            services.AddTransient<IRewardsWindowFactory, RewardsWindowFactory>();
        });

        return builder;
    }

    private static void Application_Startup(object sender, StartupEventArgs e)
    {
        // only show welcomewindow if no default pet set
        var defaultPet = PetManager.Instance.GetDefaultPet();
        if (defaultPet != null)
        {
            Host.Services.GetRequiredService<PetStatUpdater>().SetPetName(defaultPet.PetName);
            // new PetWindow(defaultPet.PetName).Show();
            ActivatorUtilities.CreateInstance<PetWindow>(Host.Services, defaultPet.PetName).Show();
        }
        else
        {
            // new WelcomeWindow().Show();
            Host.Services.GetRequiredService<WelcomeWindow>().Show();
        }
    }
}