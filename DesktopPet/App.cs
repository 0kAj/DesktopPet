using System.Windows;
using DesktopPet.Background;
using DesktopPet.Data.Pet;
using DesktopPet.Factory;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;
using DesktopPet.WPF;
using DesktopPet.WPF.GameWindows;
using DesktopPet.WPF.WindowViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesktopPet;

public class App : Application
{
    private static IHost? _host;

    public static IHost Host
    {
        get
        {
            if (_host == null)
            {
                _host = CreateHostBuilder().Build();
            }

            return _host;
        }
        private set => _host = value;
    }

    [STAThread]
    public static void Main()
    {
        App app = new App();
        app.Startup += Application_Startup;
        app.Run();

        Host.Start();
    }

    private static HostBuilder CreateHostBuilder()
    {
        var builder = new HostBuilder();
        builder.ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<WelcomeWindow>();
            services.AddTransient<WelcomeWindowViewModel>();

            services.AddTransient<GameSelectorWindow>(); //todo
            services.AddTransient<GameSelectorWindowViewModel>(); //todo PetBrain as singleton

            services.AddSingleton<PetEventManager>();
            services.AddSingleton<GameManager>();
            
            // services.AddSingleton<PetBrain>(); //todo

            services.AddTransient<RewardsWindow>();
            services.AddTransient<IRewardsWindowFactory, RewardsWindowFactory>();

            services.AddTransient<FoodCollectorMiniGameWindow>(); //todo
            services.AddTransient<FoodCollectorViewModel>();

            services.AddTransient<PetJumpMiniGameWindow>(); //todo
            services.AddTransient<PetJumpViewModel>();

            services.AddTransient<PetWindow>(); //todo
        });

        return builder;
    }

    private static void Application_Startup(object sender, StartupEventArgs e)
    {
        // only show welcomewindow if no default pet set
        var defaultPet = PetManager.Instance.GetDefaultPet();
        if (defaultPet != null)
        {
            PetStatUpdater.Instance.PetName = defaultPet.PetName;
            // new PetWindow(defaultPet.PetName).Show();
            ActivatorUtilities.CreateInstance<PetWindow>(Host.Services, defaultPet.PetName).Show();
        }
        else
        {
            // new WelcomeWindow().Show();
            Host.Services.GetService<WelcomeWindow>()!.Show();
        }
    }
}