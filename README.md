# EShopNative

A cross-platform e-commerce mobile application built with .NET MAUI, supporting Android, iOS, Mac Catalyst, and Windows platforms.

## Overview

EShopNative is a modern, native mobile application that provides a seamless shopping experience across multiple platforms. The app features user authentication, role-based access (Customer and Seller), and a clean, intuitive user interface.

## Features

- 🔐 **User Authentication** - Secure login and registration system
- 👥 **Dual Role Support** - Separate registration flows for Customers and Sellers
- 🎨 **Custom UI Components** - Reusable custom controls (BorderedEntry, BorderedPicker, CustomButton)
- 📱 **Cross-Platform** - Single codebase for Android, iOS, Mac Catalyst, and Windows
- 🏗️ **MVVM Architecture** - Clean separation of concerns using the MVVM pattern
- 🔒 **Secure Password Hashing** - BCrypt integration for password security

## Technology Stack

- **.NET 9.0** - Latest .NET framework
- **.NET MAUI** - Multi-platform App UI framework
- **CommunityToolkit.Maui** - UI components and utilities
- **CommunityToolkit.Mvvm** - MVVM helpers and base classes
- **BCrypt.Net-Next** - Password hashing library

## Supported Platforms

- ✅ Android (API 21+)
- ✅ iOS (15.0+)
- ✅ Mac Catalyst (15.0+)
- ✅ Windows (10.0.17763.0+)
- ⚠️ Tizen (commented out, requires additional setup)

## Project Structure

```
EShopNative/
├── BaseLibrary/          # Base classes and utilities
├── Converters/           # Value converters for XAML
├── CustomViews/          # Custom UI components
├── Enums/                # Application enumerations
├── Models/               # Data models
├── Pages/                # Application pages
├── Platforms/            # Platform-specific implementations
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   ├── Tizen/
│   └── Windows/
├── Resources/            # Images, fonts, and assets
├── Services/             # Business logic services
├── ViewModels/           # ViewModels for MVVM pattern
└── Views/                # XAML views
```

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.8 or later) with:
  - .NET Multi-platform App UI development workload
  - Platform-specific SDKs for your target platforms:
    - Android SDK (for Android development)
    - Xcode (for iOS/Mac Catalyst development on macOS)
    - Windows SDK (for Windows development)

## Getting Started

### Clone the Repository

```bash
git clone <repository-url>
cd EShopNative
```

### Restore Dependencies

```bash
dotnet restore
```

### Build the Project

```bash
dotnet build
```

### Run the Application

#### Android
```bash
dotnet build -t:Run -f net9.0-android
```

#### iOS (macOS only)
```bash
dotnet build -t:Run -f net9.0-ios
```

#### Mac Catalyst (macOS only)
```bash
dotnet build -t:Run -f net9.0-maccatalyst
```

#### Windows
```bash
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

Alternatively, you can run the application directly from Visual Studio by selecting your target platform and pressing F5.

## Application Flow

1. **Welcome Screen** - Initial entry point
2. **User Role Selection** - Choose between Customer or Seller
3. **Authentication** - Login or Registration
4. **Home** - Main application interface (after authentication)

## Custom Components

The application includes several custom UI components:

- **BorderedEntry** - Custom entry field with border styling
- **BorderedPicker** - Custom picker with border styling
- **CustomButton** - Custom button component
- **CustomNavBar** - Custom navigation bar

## Configuration

### Application Settings

- **Application ID**: `com.companyname.eshopnative`
- **Application Title**: `EShopNative`
- **Version**: 1.0

### Platform-Specific Requirements

- **Android**: Minimum API level 21 (Android 5.0)
- **iOS**: Minimum iOS 15.0
- **Mac Catalyst**: Minimum macOS 15.0
- **Windows**: Minimum Windows 10 version 1809

## Dependencies

- `BCrypt.Net-Next` (4.0.3) - Password hashing
- `CommunityToolkit.Maui` (12.0.5) - MAUI toolkit
- `CommunityToolkit.Mvvm` (8.4.0) - MVVM helpers
- `Microsoft.Extensions.Logging.Console` (10.0.1) - Console logging
- `Microsoft.Extensions.Logging.Debug` (9.0.8) - Debug logging

## Development

### Adding New Views

1. Create a new XAML file in the `Views/` directory
2. Create a corresponding code-behind file (`.xaml.cs`)
3. Create a ViewModel in the `ViewModels/` directory
4. Register the view in your navigation flow

### Custom Handlers

The application includes custom handlers for Entry and Picker controls to remove default platform-specific underlines. These are configured in `MauiProgram.cs`.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

[Specify your license here]

## Contact

[Add contact information or project maintainer details]

---

Built with ❤️ using .NET MAUI
