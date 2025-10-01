namespace RemindaBot.Core;

public class Reminder
{
    // Unieke ID om de reminder te kunnen bewerken/verwijderen
    public Guid Id { get; init; } = Guid.NewGuid(); 
    
    // De tekst van de notificatie
    public string Title { get; set; }
    public string Message { get; set; }
    
    // De frequentie (bijv. 1 uur, 30 minuten)
    public TimeSpan Interval { get; set; } 
    
    // Tijdstip van de laatste daadwerkelijke notificatie (voor de timer-logica)
    public DateTime LastNotified { get; set; } 
    
    // Of de reminder actief is (user editable setting)
    public bool IsActive { get; set; } = true;
}