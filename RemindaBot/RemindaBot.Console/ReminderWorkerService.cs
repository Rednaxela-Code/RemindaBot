using Microsoft.Extensions.Hosting;
using RemindaBot.Core;

namespace RemindaBot.Console;

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
        System.Console.WriteLine("--- RemindaBot Worker Gestart ---");
        
        // Starting timer this instance (TimeSpan.Zero) and repeat every x seconds.
        _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1)); 
        
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
            bool firstRun = true;
            
            // Get existing reminders
            var messages = (await _reminderManager.GetAllRemindersAsync()).ToList();

            // Check if list is empty, if empty make test one.
            if (firstRun && messages.Count < 1 || messages.Count < 1)
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
                firstRun = false;
            }

            // Do central logic
            await _reminderManager.CheckAndNotifyAsync();
        }
        catch (Exception ex)
        {
            // TODO: Log with usefull somethign
            System.Console.WriteLine($"Een fout is opgetreden in de worker service: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        System.Console.WriteLine("--- RemindaBot Worker Gestopt ---");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}