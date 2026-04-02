# ⚔️ UpUpUp

<p align="center">
  <a href="./README.zh-CN.md">中文</a> | <b>English</b>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20MAUI-512BD4?style=for-the-badge&logo=.net&logoColor=white" alt=".NET MAUI">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white" alt="XAML">
  <img src="https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android">
  <img src="https://img.shields.io/badge/iOS-000000?style=for-the-badge&logo=ios&logoColor=white" alt="iOS">
  <img src="https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white" alt="Windows">
  <img src="https://img.shields.io/badge/macOS-000000?style=for-the-badge&logo=apple&logoColor=white" alt="macOS">
</p>

<p align="center">
  <b>A card battling game inspired by Slay the Spire</b><br>
  Strategy, luck, and courage - how far can you climb?
</p>

---

## 🎮 Game Introduction

**UpUpUp** is a turn-based card battling game. You play as a brave adventurer, armed with cards, climbing higher and higher, challenging increasingly powerful enemies.

Each level is a new challenge. Every card could be your chance to turn the tables. Every decision will determine your fate.

## ✨ Core Features

### 🎴 Card Combat System

Build your deck and use attack and skill cards to outmaneuver your enemies. With countless card combinations, find your ultimate strategy.

### 🏰 Level Progression System

Unlock more powerful cards as you advance through levels. Higher levels bring greater rewards, but also more dangerous foes.

### ⚔️ Intelligent Enemy AI

Enemies have different combat intents: Attack, Defend, and Charge. Before each round begins, you'll see the enemy's next move, allowing you to prepare accordingly.

### 🎒 Deck Cycling Mechanic

Classic draw pile and discard pile cycling design. Plan your hand usage wisely and deliver the finishing blow at the critical moment.

### 🎯 Deep Strategic Gameplay

Go all-out for a quick victory, or play it safe with defensive counter-attacks? Every turn requires careful thought.

---

## 🖼️ Game Screenshots

<p align="center">

### 🎴 Battle Scene
```
┌─────────────────────────────────────────┐
│  HP: ❤️❤️❤️❤️❤️❤️❤️❤️                     │
│  Block: 🛡️🛡️                            │
│                                         │
│         👹 Enemy (Intent: ⚔️ Attack)     │
│              HP: 25/30                  │
│                                         │
│  ─────────────────────────────────────  │
│                                         │
│  [Strike] [Defend] [Strike] [Bash]      │
│                                         │
│   Draw: 12  |  Discard: 5  |  Energy: 3 │
└─────────────────────────────────────────┘
```

### 📊 Card Catalog

| Card Name | Type | Effect | Unlock Level |
|---------|------|------|---------|
| **Strike** | Attack | Deal 6 damage | Level 1 |
| **Defend** | Skill | Gain 5 Block | Level 1 |
| **Bash** | Attack | Deal 8 damage | Level 2 |
| **Heavy Strike** | Attack | Deal 12 damage | Level 2 |
| **Power Slash** | Attack | Deal 15 damage | Level 3 |
| **Iron Skin** | Skill | Gain 12 Block | Level 3 |

</p>

---

## 🚀 Quick Start

### Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or higher
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or Visual Studio Code
- Android / iOS emulator or physical device (for mobile testing)

### Installation

1. **Clone the repository**

```bash
git clone https://github.com/yourusername/UpUpUp.git
cd UpUpUp
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Run the project**

**Windows / macOS:**
```bash
dotnet build
dotnet run
```

**Android (requires connected device or emulator):**
```bash
dotnet build -t:Run -f net8.0-android
```

**iOS (requires macOS and Xcode):**
```bash
dotnet build -t:Run -f net8.0-ios
```

### Running with Visual Studio

1. Open the `UpUpUp.sln` file
2. Select target platform from the toolbar (Windows / Android / iOS / macOS)
3. Press `F5` or click the Run button

---

## 📁 Project Structure

```
UpUpUp/
├── 📱 Platforms/              # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── 🎨 Resources/              # Application resources
│   ├── Images/               # Image assets
│   ├── Raw/                  # Raw resources
│   └── Styles/               # Style definitions
├── 🧩 Models/                 # Data models
│   ├── Card.cs               # Card model
│   ├── Enemy.cs              # Enemy model
│   └── Player.cs             # Player model
├── 🎮 ViewModels/             # View models (MVVM)
│   ├── GameViewModel.cs      # Game logic
│   └── CardViewModel.cs      # Card logic
├── 🔄 Converters/             # Value converters
├── 📄 App.xaml               # Application entry
├── 📄 MainPage.xaml          # Main page
├── 📄 StartPage.xaml         # Start page
└── ⚙️ MauiProgram.cs         # MAUI configuration
```

---

## 🎮 Gameplay

### Basic Rules

1. **Turn-based Combat**: You and enemies take turns
2. **Energy System**: Gain fixed energy each turn; playing cards consumes energy
3. **Hand Mechanic**: Draw fixed number of cards from draw pile each turn
4. **Deck Cycling**: When draw pile is empty, discard pile shuffles to become new draw pile

### Combat Flow

```
Start Turn → Draw Cards → View Enemy Intent → Play Cards → End Turn → Enemy Action → Loop
```

### Victory Condition

Defeat all enemies in the current level to advance to the next. Each level's enemies become stronger, but you'll also gain more powerful cards.

---

## 🛠️ Tech Stack

- **[.NET MAUI](https://dotnet.microsoft.com/apps/maui)** - Cross-platform UI framework
- **[C#](https://docs.microsoft.com/dotnet/csharp/)** - Programming language
- **[XAML](https://docs.microsoft.com/xamarin/xamarin-forms/xaml/)** - UI markup language
- **[MVVM](https://docs.microsoft.com/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)** - Architecture pattern

---

## 📜 License

This project is open source under the [MIT License](LICENSE).

---

## 🤝 Contributing

Issues and Pull Requests are welcome!

1. Fork this repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 🙏 Acknowledgments

- Inspired by [Slay the Spire](https://www.megacrit.com/)
- Thanks to the .NET MAUI team for the excellent cross-platform framework

---

<p align="center">
  <b>⭐ If this project helps you, please give it a Star!</b>
</p>

<p align="center">
  🎴 Ready your cards and start climbing! ⚔️
</p>
