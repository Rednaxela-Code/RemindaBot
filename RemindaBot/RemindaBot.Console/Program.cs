using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemindaBot.Core;
using RemindaBot.Infrastructure;

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
                // 1. Core Service Registration (Logic)
                services.AddSingleton<IReminderManager, ReminderManager>();
                
                // NotificationManager for handling extenal library for system notifications
                //services.AddSingleton<INotificationManager, DesktopNotificationManager>();

                // 2. Infrastructure Implementations Registration
                services.AddSingleton<INotificationService, CrossPlatformNotificationService>();
                
                // IReminderRepository:
                services.AddSingleton<IReminderRepository, JsonReminderRepository>();

                // 3. De Worker Service (Keeps console app alive and works the timer)
                services.AddHostedService<ReminderWorkerService>();
            });
}

public class ReminderWorkerService : IHostedService, IDisposable
{
    private readonly IReminderManager _reminderManager;
    private readonly Timer _timer;
    
    public ReminderWorkerService(IReminderManager reminderManager)
    {
        _reminderManager = reminderManager;
        // Initialise timer but do not run it
        _timer = new Timer(DoWork, null, Timeout.Infinite, Timeout.Infinite);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("--- RemindaBot Worker Gestart ---");
        
        // Starting timer this instance (TimeSpan.Zero) and repeat every x seconds.
        _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5)); 
        
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        // the timer is synchronous, using a wrapper around async method so the timer does not get blocked by asynchronous tasks
        _ = ExecuteWorkAsync();
    }

    private async Task ExecuteWorkAsync()
    {
        try
        {
            // Get existing reminders
            var messages = (await _reminderManager.GetAllRemindersAsync()).ToList();

            // Check if list is empty, if empty make test one.
            if (messages.Count < 1)
            {
                var reminder = new Reminder()
                {
                    Title = "Test",
                    Message = "Test Message for Test",
                    Interval = new TimeSpan(0, 0, 10),
                    LastNotified = DateTime.Now,
                    IsActive = true,
                };

                await _reminderManager.SaveReminderAsync(reminder);
            }

            // Do central logic
            await _reminderManager.CheckAndNotifyAsync();
        }
        catch (Exception ex)
        {
            // TODO: Log with usefull somethign
            Console.WriteLine($"Een fout is opgetreden in de worker service: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        Console.WriteLine("--- RemindaBot Worker Gestopt ---");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}