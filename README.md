# Reminder Bot (MAUI Blazor Hybrid Application)

## Overview
The **Reminder Bot** is a lightweight cross-platform application written in `.NET 8` (future plans for `.NET 9`). It helps users stay productive and maintain healthy habits by sending periodic reminders with priority-based notifications. Users can customize reminders, configure notification priorities, and snooze reminders with multiple options.

---

## Features
1. **Cross-Platform Support**:
   - Runs on Windows, macOS, iOS, and Android.
   - Utilizes .NET MAUI Blazor Hybrid for a web-like UI with native functionality.

2. **Customizable Reminders**:
   - Configure reminders with custom messages, intervals, and priorities.
   - Save settings in a JSON file for persistence.

3. **Priority-Based Notifications**:
   - Notifications styled differently based on priority (e.g., Low, Medium, High).

4. **Snooze Options**:
   - Postpone notifications by 5, 10, or 15 minutes.

5. **UI for Settings**:
   - Blazor-based interface to manage reminders and their schedules.

6. **Unit Testing**:
   - Solid test coverage for core functionality to ensure reliability.

---

## Requirements
- **.NET 8 SDK** (will migrate to .NET 9 in the future).
- Windows 10/11, macOS, iOS, or Android.

---

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/reminder-bot.git
   cd reminder-bot
   ```
2. Build and run the application:
   ```bash
   dotnet build
   dotnet run
   ```
3. Optionally, publish as a standalone app for a specific platform:
   ```bash
   dotnet publish -c Release -r win10-x64 --self-contained
   ```

---

## Usage
### System Tray (Windows-Specific)
- On Windows, the bot runs silently in the system tray after launch.
- Right-click the tray icon to access the context menu for settings and exiting the application.

### Settings UI
- Open the settings UI to manage reminders.
- Configure:
  - **Message**: The text displayed in the notification.
  - **Interval**: Time interval between reminders.
  - **Priority**: Choose between Low, Medium, and High.
  - **Snooze Options**: Customize snooze durations.

### JSON Configuration
Settings are stored in a `settings.json` file for persistence. Example structure:
```json
{
  "Reminders": [
    {
      "Message": "Drink Water",
      "IntervalMinutes": 60,
      "Priority": "High"
    },
    {
      "Message": "Stretch",
      "IntervalMinutes": 90,
      "Priority": "Medium"
    }
  ]
}
```

---

## Notification Customization
### Priority-Based Styling
- **High Priority**: Bold and red text in the notification.
- **Medium Priority**: Standard text.
- **Low Priority**: Greyed-out text.

### Snooze Options
Snooze the current notification for:
- 5 minutes.
- 10 minutes.
- 15 minutes.

---

## Development
### Prerequisites
- Install the `.NET 8 SDK`.
- Use Visual Studio or your preferred IDE for .NET development.

### Key Libraries
- [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications): For toast notifications.
- [Plugin.LocalNotification](https://www.nuget.org/packages/Plugin.LocalNotification): For cross-platform local notifications.

### Running Tests
Run unit tests using the following command:
```bash
dotnet test
```

---

## Roadmap
1. **Migrate to .NET 9** once officially released.
2. **Enhanced Notification Actions**:
   - Add more dynamic buttons to notifications.
3. **Multi-User Support**:
   - Enable reminders for multiple users on the same system.
4. **Cloud Sync**:
   - Store settings in the cloud for cross-device syncing.

---

## Contributing
1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit changes:
   ```bash
   git commit -m "Description of changes"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request.

---

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Acknowledgements
- [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications): For making toast notifications simple.
- [Plugin.LocalNotification](https://www.nuget.org/packages/Plugin.LocalNotification): For cross-platform notification support.
- The .NET community for tools and libraries.

