# ⚔️ UpUpUp

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
  <b>一款致敬《杀戮尖塔》的卡牌战斗游戏</b><br>
  策略、运气、勇气 - 你能爬到第几层？
</p>

---

## 🎮 游戏简介

**UpUpUp** 是一款回合制卡牌战斗游戏。你将扮演一名勇敢的冒险者，手持卡牌，一层一层地向上攀登，挑战越来越强大的敌人。

每一关都是新的挑战。每一张牌都可能是翻盘的契机。每一次抉择都将决定你的命运。

## ✨ 核心特色

### 🎴 卡牌战斗系统

构筑你的牌组，运用攻击卡和防御卡与敌人周旋。卡牌组合千变万化，找到属于你的最强流派。

### 🏰 关卡进阶系统

随着关卡提升，解锁更强力的卡牌。关卡越高，奖励越丰厚，但敌人也越危险。

### ⚔️ 智能敌人AI

敌人拥有不同的战斗意图：攻击、防御、蓄力。每回合开始前，你会看到敌人的下一步行动，提前做好应对准备。

### 🎒 牌库循环机制

经典的抽牌堆、弃牌堆循环设计。合理规划手牌使用，在关键时刻打出致命一击。

### 🎯 深度策略玩法

是全力进攻速战速决？还是稳扎稳打防守反击？每个回合都需要深思熟虑。

---

## 🖼️ 游戏画面

<p align="center">

### 🎴 战斗场景
```
┌─────────────────────────────────────────┐
│  HP: ❤️❤️❤️❤️❤️❤️❤️❤️                     │
│  Block: 🛡️🛡️                            │
│                                         │
│         👹 敌人 (意图: ⚔️ 攻击)          │
│              HP: 25/30                  │
│                                         │
│  ─────────────────────────────────────  │
│                                         │
│  [Strike] [Defend] [Strike] [Bash]      │
│                                         │
│   抽牌堆: 12  |  弃牌堆: 5  |  能量: 3   │
└─────────────────────────────────────────┘
```

### 📊 卡牌图鉴

| 卡牌名称 | 类型 | 效果 | 解锁关卡 |
|---------|------|------|---------|
| **Strike** | 攻击 | 造成 6 点伤害 | 第 1 关 |
| **Defend** | 技能 | 获得 5 点格挡 | 第 1 关 |
| **Bash** | 攻击 | 造成 8 点伤害 | 第 2 关 |
| **Heavy Strike** | 攻击 | 造成 12 点伤害 | 第 2 关 |
| **Power Slash** | 攻击 | 造成 15 点伤害 | 第 3 关 |
| **Iron Skin** | 技能 | 获得 12 点格挡 | 第 3 关 |

</p>

---

## 🚀 快速开始

### 环境要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) 或更高版本
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (推荐) 或 Visual Studio Code
- Android / iOS 模拟器或真机 (移动端测试)

### 安装步骤

1. **克隆仓库**

```bash
git clone https://github.com/yourusername/UpUpUp.git
cd UpUpUp
```

2. **还原依赖**

```bash
dotnet restore
```

3. **运行项目**

**Windows / macOS:**
```bash
dotnet build
dotnet run
```

**Android (需要连接设备或启动模拟器):**
```bash
dotnet build -t:Run -f net8.0-android
```

**iOS (需要 macOS 和 Xcode):**
```bash
dotnet build -t:Run -f net8.0-ios
```

### Visual Studio 运行

1. 打开 `UpUpUp.sln` 文件
2. 在顶部工具栏选择目标平台 (Windows / Android / iOS / macOS)
3. 按 `F5` 或点击运行按钮

---

## 📁 项目结构

```
UpUpUp/
├── 📱 Platforms/              # 平台特定代码
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── 🎨 Resources/              # 应用资源
│   ├── Images/               # 图片资源
│   ├── Raw/                  # 原始资源
│   └── Styles/               # 样式定义
├── 🧩 Models/                 # 数据模型
│   ├── Card.cs               # 卡牌模型
│   ├── Enemy.cs              # 敌人模型
│   └── Player.cs             # 玩家模型
├── 🎮 ViewModels/             # 视图模型 (MVVM)
│   ├── GameViewModel.cs      # 游戏逻辑
│   └── CardViewModel.cs      # 卡牌逻辑
├── 🔄 Converters/             # 值转换器
├── 📄 App.xaml               # 应用入口
├── 📄 MainPage.xaml          # 主页面
├── 📄 StartPage.xaml         # 启动页面
└── ⚙️ MauiProgram.cs         # MAUI 配置
```

---

## 🎮 游戏玩法

### 基本规则

1. **回合制战斗**: 你和敌人轮流行动
2. **能量系统**: 每回合获得固定能量，打出卡牌消耗能量
3. **手牌机制**: 每回合从抽牌堆抽取固定数量的卡牌
4. **牌库循环**: 抽牌堆耗尽时，弃牌堆洗牌后成为新的抽牌堆

### 战斗流程

```
开始回合 → 抽牌 → 查看敌人意图 → 打出卡牌 → 结束回合 → 敌人行动 → 循环
```

### 胜利条件

击败当前关卡所有敌人即可进入下一关。每关敌人会更强，但你也会获得更强力的卡牌。

---

## 🛠️ 技术栈

- **[.NET MAUI](https://dotnet.microsoft.com/apps/maui)** - 跨平台 UI 框架
- **[C#](https://docs.microsoft.com/dotnet/csharp/)** - 编程语言
- **[XAML](https://docs.microsoft.com/xamarin/xamarin-forms/xaml/)** - UI 标记语言
- **[MVVM](https://docs.microsoft.com/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)** - 架构模式

---

## 📜 开源协议

本项目采用 [MIT 协议](LICENSE) 开源。

---

## 🤝 贡献指南

欢迎提交 Issue 和 Pull Request！

1. Fork 本仓库
2. 创建你的功能分支 (`git checkout -b feature/amazing-feature`)
3. 提交更改 (`git commit -m 'Add amazing feature'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 打开 Pull Request

---

## 🙏 致谢

- 灵感来源于 [Slay the Spire](https://www.megacrit.com/) (杀戮尖塔)
- 感谢 .NET MAUI 团队提供的优秀跨平台框架

---

<p align="center">
  <b>⭐ 如果这个项目对你有帮助，请给个 Star 支持一下！</b>
</p>

<p align="center">
  🎴 准备好你的卡牌，开始攀登吧！ ⚔️
</p>
