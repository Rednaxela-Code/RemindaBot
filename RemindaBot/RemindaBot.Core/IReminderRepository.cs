using RemindaBot.Core;

namespace RemindaBot.Core;

public interface IReminderRepository
{
    Task<IEnumerable<Reminder>> GetAllRemindersAsync();
    Task SaveReminderAsync(Reminder reminder); // Voor opslaan of updaten
}