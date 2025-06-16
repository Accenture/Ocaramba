# Ocaramba.Tests.Appium

This project demonstrates how to run Appium-based mobile tests using NUnit and C#.

## Features

- Example Android Appium test using NUnit
- Configurable Appium settings in `appsettings.json`

## Prerequisites

- .NET 6.0 SDK or later
- Appium server running (see [Appium documentation](https://appium.io/))
- Android emulator or real device available
- The sample app APK available at the path specified in `appsettings.json`

## How to Run

1. Start the Appium server:
   ```pwsh
   appium
   ```
2. Ensure your Android emulator or device is running and accessible.
3. Update `appsettings.json` with the correct app path and device details if needed.
4. Run the tests:
   ```pwsh
   dotnet test
   ```

## Notes

- The sample test in `UnitTest1.cs` uses a placeholder element ID. Update it to match your app.
- For more advanced configuration, see the [Appium documentation](https://appium.io/docs/en/about-appium/intro/).
