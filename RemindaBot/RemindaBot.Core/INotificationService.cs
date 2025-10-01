namespace RemindaBot.Core;

public interface INotificationService
{
    /// <summary>
    /// Asks the infrastructure layer to push a system notification.
    /// </summary>
    /// <param name="title">Title of notification.</param>
    /// <param name="message">Main content of notification.</param>
    void PushNotification(string title, string message);
}