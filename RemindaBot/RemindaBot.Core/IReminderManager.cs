namespace RemindaBot.Core;

public interface IReminderManager
{
    Task<IEnumerable<Reminder>> GetAllRemindersAsync();
    Task SaveReminderAsync(Reminder reminder);
    Task CheckAndNotifyAsync(); 
}