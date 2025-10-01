namespace RemindaBot.Core;

public class ReminderManager : IReminderManager
{
    private readonly INotificationService _notificationService;
    private readonly IReminderRepository _repository; 
    
    public ReminderManager(INotificationService notificationService, IReminderRepository repository)
    {
        _notificationService = notificationService;
        _repository = repository;
    }

    public Task<IEnumerable<Reminder>> GetAllRemindersAsync()
    {
        return _repository.GetAllRemindersAsync();
    }

    public async Task SaveReminderAsync(Reminder reminder)
    {
        await _repository.SaveReminderAsync(reminder);
    }

    public async Task CheckAndNotifyAsync()
    {
        var reminders = await _repository.GetAllRemindersAsync();

        foreach (var reminder in reminders.Where(r => r.IsActive))
        {
            if (DateTime.Now - reminder.LastNotified >= reminder.Interval)
            {
                _notificationService.PushNotification(reminder.Title, reminder.Message);
                reminder.LastNotified = DateTime.Now;
                await _repository.SaveReminderAsync(reminder);
            }
        }
    }
}