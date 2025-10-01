using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemindaBot.Core;
using RemindaBot.Infrastructure;

namespace RemindaBot.Console;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Core Service Registration (Logic)
                services.AddSingleton<IReminderManager, ReminderManager>();
                
                // NotificationManager for handling extenal library for system notifications
                //services.AddSingleton<INotificationManager, DesktopNotificationManager>();

                // Infrastructure Implementations Registration
                services.AddSingleton<INotificationService, CrossPlatformNotificationService>();
                
                // IReminderRepository:
                services.AddSingleton<IReminderRepository, JsonReminderRepository>();

                // De Worker Service (Keeps console app alive and works the timer)
                services.AddHostedService<ReminderWorkerService>();
            });
}