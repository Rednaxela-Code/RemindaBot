using RemindaBot.Core;

namespace RemindaBot.Infrastructure;

public class CrossPlatformNotificationService : INotificationService
{
    // This will be the API that handles library for system notifications
    //private readonly INotificationManager _notificationManager;

    // The notificationManager will be injected via di container
    public CrossPlatformNotificationService(/* INotificationManager notificationManager */)
    {
        
        // _notificationManager = notificationManager;
        
        // For now just a CW message
        Console.WriteLine("CrossPlatformNotificationService geïnitialiseerd.");
    }

    public void PushNotification(string title, string message)
    {
        try
        {
            // For now just the CW note, but should contain the impl for _notificationManager function
            Console.WriteLine($"INFRA - NOTIFICATIE VERSTUURD: [{title}] {message}");
        }
        catch (Exception ex)
        {
            // TODO: Do usefull logging.
            Console.WriteLine($"Fout bij versturen notificatie: {ex.Message}");
        }
    }
}