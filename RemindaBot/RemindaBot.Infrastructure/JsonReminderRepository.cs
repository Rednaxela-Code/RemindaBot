using System.Text.Json;
using RemindaBot.Core;

namespace RemindaBot.Infrastructure;

public class JsonReminderRepository : IReminderRepository
{
    public Task<IEnumerable<Reminder>> GetAllRemindersAsync()
    {
        const string fileName = "Reminders.json";
        
        if (!File.Exists(fileName))
        {
            return Task.FromResult<IEnumerable<Reminder>>([]);
        }
        
        var json = File.ReadAllText(fileName);
        var list = JsonSerializer.Deserialize<List<Reminder>>(json);

        return Task.FromResult<IEnumerable<Reminder>>(list ?? []);
    }

    public async Task SaveReminderAsync(Reminder reminder)
    {
        // Get all reminders Then update current collectioin with incoming one added or updated.
        const string fileName = "Reminders.json";

        var reminders = (await GetAllRemindersAsync()).ToList();

        var existing = reminders.FirstOrDefault(r => r.Id == reminder.Id);

        if (existing != null)
        {
            // Update
            existing.Title = reminder.Title;
            existing.Message = reminder.Message;
            existing.Interval = reminder.Interval;
            existing.LastNotified = reminder.LastNotified;
            existing.IsActive = reminder.IsActive;
        }
        else
        {
            // Add to collection
            reminders.Add(reminder);
        }

        var json = JsonSerializer.Serialize(reminders);
        await File.WriteAllTextAsync(fileName, json);
    }
}