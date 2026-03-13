using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UpUpUp.Models;

namespace UpUpUp.ViewModels
{
    public partial class BattleViewModel : ObservableObject
    {
        // 玩家属性
        [ObservableProperty]
        private int playerHP;
        
        [ObservableProperty]
        private int playerMaxHP = 50;
        
        [ObservableProperty]
        private int playerBlock;
        
        // 关卡系统
        [ObservableProperty]
        private int currentLevel = 1;
        
        [ObservableProperty]
        private int totalLevels = 3;
        
        [ObservableProperty]
        private string levelText = "第1关";
        
        // 敌人管理
        public ObservableCollection<EnemyModel> Enemies { get; } = new();
        
        [ObservableProperty]
        private EnemyModel? selectedEnemy;
        
        [ObservableProperty]
        private bool isBattleWon = false;
        
        [ObservableProperty]
        private bool isGameOver = false;
        
        [ObservableProperty]
        private bool isVictory = false;
        
        [ObservableProperty]
        private string battleStatusText = "";
        
        // 卡牌
        public ObservableCollection<CardModel> HandCards { get; } = new();
        private List<CardModel> deckCards = new();  // 抽牌堆
        private List<CardModel> discardPile = new();  // 弃牌堆
        
        // 抽牌堆数量
        [ObservableProperty]
        private int drawPileCount;
        
        // 弃牌堆数量
        [ObservableProperty]
        private int discardPileCount;
        
        // 全部卡牌（用于预览）
        public ObservableCollection<CardModel> AllCards { get; } = new();
        
        // 是否显示牌库预览
        [ObservableProperty]
        private bool isDeckPreviewVisible = false;
        
        public BattleViewModel()
        {
            // 初始化玩家
            this.PlayerMaxHP = 50;
            this.PlayerHP = PlayerMaxHP;
            this.PlayerBlock = 0;
            this.CurrentLevel = 1;
            
            // 初始化第一关
            InitializeLevel();
            
            // 初始化卡牌库
            InitializeDeck();
            DrawCards(3);
        }
        
        public BattleViewModel(int playerHP, int level)
        {
            // 继续游戏时使用
            this.PlayerMaxHP = playerHP;
            this.PlayerHP = playerHP;
            this.PlayerBlock = 0;
            this.CurrentLevel = level;
            
            // 初始化当前关卡
            InitializeLevel();
            
            // 初始化卡牌库
            InitializeDeck();
            DrawCards(3);
        }
        
        private void InitializeLevel()
        {
            LevelText = CurrentLevel switch
            {
                1 => "第1关",
                2 => "第2关",
                3 => "第3关 - BOSS战",
                _ => $"第{CurrentLevel}关"
            };
            
            // 根据关卡生成敌人
            Enemies.Clear();
            
            switch (CurrentLevel)
            {
                case 1:
                    // 第一关：1-2个普通敌人
                    Enemies.Add(CreateEnemy(EnemyType.Normal, "哥布林", 25));
                    if (new Random().Next(2) == 0)
                    {
                        Enemies.Add(CreateEnemy(EnemyType.Normal, "骷髅兵", 20));
                    }
                    break;
                    
                case 2:
                    // 第二关：1精英+1普通
                    Enemies.Add(CreateEnemy(EnemyType.Elite, "精英骷髅", 45));
                    Enemies.Add(CreateEnemy(EnemyType.Normal, "骷髅兵", 20));
                    break;
                    
                case 3:
                    // 第三关：Boss
                    Enemies.Add(CreateEnemy(EnemyType.Boss, "炎魔领主", 80));
                    break;
            }
            
            // 选第一个活着的敌人
            SelectFirstAliveEnemy();
            
            // 为所有敌人生成意图
            foreach (var enemy in Enemies)
            {
                enemy.GenerateIntent();
            }
        }
        
        private EnemyModel CreateEnemy(EnemyType type, string name, int hp)
        {
            return new EnemyModel
            {
                Name = name,
                Type = type,
                MaxHP = hp,
                CurrentHP = hp,
                Block = 0
            };
        }
        
        private void SelectFirstAliveEnemy()
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.IsAlive)
                {
                    SelectedEnemy = enemy;
                    return;
                }
            }
            SelectedEnemy = null;
        }
        
        private void InitializeDeck()
        {
            deckCards.Clear();
            discardPile.Clear();
            
            // 根据关卡增加更强卡牌
            for (int i = 0; i < 3; i++)
            {
                deckCards.Add(new CardModel
                {
                    Name = "Strike",
                    Description = "对敌人造成6点伤害。",
                    Damage = 6,
                    Block = 0,
                    Cost = 1
                });
                
                deckCards.Add(new CardModel
                {
                    Name = "Defend",
                    Description = "获得5点格挡。",
                    Damage = 0,
                    Block = 5,
                    Cost = 1
                });
            }
            
            // 第二关开始增加更强卡牌
            if (CurrentLevel >= 2)
            {
                deckCards.Add(new CardModel
                {
                    Name = "Bash",
                    Description = "对敌人造成8点伤害。",
                    Damage = 8,
                    Block = 0,
                    Cost = 2
                });
                
                deckCards.Add(new CardModel
                {
                    Name = "Heavy Strike",
                    Description = "对敌人造成12点伤害。",
                    Damage = 12,
                    Block = 0,
                    Cost = 2
                });
            }
            
            // 第三关增加更多强力卡牌
            if (CurrentLevel >= 3)
            {
                deckCards.Add(new CardModel
                {
                    Name = "Power Slash",
                    Description = "对敌人造成15点伤害。",
                    Damage = 15,
                    Block = 0,
                    Cost = 3
                });
                
                deckCards.Add(new CardModel
                {
                    Name = "Iron Skin",
                    Description = "获得12点格挡。",
                    Damage = 0,
                    Block = 12,
                    Cost = 2
                });
            }
            
            // 更新全部卡牌预览
            UpdateAllCards();
            
            ShuffleDeck();
            UpdatePileCounts();
        }
        
        private void UpdateAllCards()
        {
            AllCards.Clear();
            foreach (var card in deckCards)
            {
                AllCards.Add(card);
            }
        }
        
        private void UpdatePileCounts()
        {
            DrawPileCount = deckCards.Count;
            DiscardPileCount = discardPile.Count;
        }
        
        private void ShuffleDeck()
        {
            Random random = new Random();
            for (int i = deckCards.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                (deckCards[i], deckCards[randomIndex]) = (deckCards[randomIndex], deckCards[i]);
            }
        }
        
        private void DrawCards(int count)
        {
            for (int i = 0; i < count && deckCards.Count > 0; i++)
            {
                var card = deckCards[0];
                deckCards.RemoveAt(0);
                HandCards.Add(card);
            }
            UpdatePileCounts();
        }
        
        // 切换牌库预览显示
        [RelayCommand]
        private void ToggleDeckPreview()
        {
            IsDeckPreviewVisible = !IsDeckPreviewVisible;
        }
        
        // 关闭牌库预览
        [RelayCommand]
        private void CloseDeckPreview()
        {
            IsDeckPreviewVisible = false;
        }
        
        [RelayCommand]
        private void SelectEnemy(EnemyModel? enemy)
        {
            if (enemy != null && enemy.IsAlive)
            {
                SelectedEnemy = enemy;
            }
        }
        
        // 拖拽攻击 - 对指定敌人造成伤害（仅攻击牌）
        public void DragAttack(EnemyModel targetEnemy, CardModel card)
        {
            if (targetEnemy == null || card == null || !targetEnemy.IsAlive)
                return;
            
            // 对目标敌人造成伤害
            targetEnemy.TakeDamage(card.Damage);
            
            // 玩家获得格挡
            PlayerBlock += card.Block;
            
            BattleStatusText = $"使用 {card.Name} 攻击 {targetEnemy.Name}!";
            
            // 检查敌人是否死亡
            if (!targetEnemy.IsAlive)
            {
                BattleStatusText = $"{targetEnemy.Name} 被击败!";
                
                // 从敌人列表中移除死亡敌人
                Enemies.Remove(targetEnemy);
                
                // 选择下一个活着的敌人
                SelectFirstAliveEnemy();
                
                // 如果没有活着的敌人，检查是否过关
                if (SelectedEnemy == null)
                {
                    HandleEnemyDefeated();
                }
            }
            
            // 从手牌移除并加入弃牌堆
            if (HandCards.Contains(card))
            {
                HandCards.Remove(card);
                discardPile.Add(card);
            }
            
            UpdatePileCounts();
        }
        
        // 使用防御卡牌（拖拽到任意位置）
        public void UseDefendCard(CardModel card)
        {
            if (card == null)
                return;
            
            // 玩家获得格挡
            PlayerBlock += card.Block;
            
            BattleStatusText = $"使用 {card.Name} 获得 {card.Block} 点护甲!";
            
            // 从手牌移除并加入弃牌堆
            if (HandCards.Contains(card))
            {
                HandCards.Remove(card);
                discardPile.Add(card);
            }
            
            UpdatePileCounts();
        }
        
        [RelayCommand]
        private void PlayCard(CardModel? card)
        {
            if (card == null || SelectedEnemy == null || !SelectedEnemy.IsAlive)
                return;
            
            // 对选中敌人造成伤害
            SelectedEnemy.TakeDamage(card.Damage);
            
            // 玩家获得格挡
            PlayerBlock += card.Block;
            
            // 检查敌人是否死亡
            if (!SelectedEnemy.IsAlive)
            {
                // 保存敌人名字
                string enemyName = SelectedEnemy.Name;
                
                // 从敌人列表中移除死亡敌人
                Enemies.Remove(SelectedEnemy);
                
                // 选择下一个活着的敌人
                SelectFirstAliveEnemy();
                
                BattleStatusText = $"{enemyName} 被击败!";
                
                // 如果没有活着的敌人，检查是否过关
                if (SelectedEnemy == null)
                {
                    HandleEnemyDefeated();
                }
            }
            
            // 从手牌移除并加入弃牌堆
            if (HandCards.Contains(card))
            {
                HandCards.Remove(card);
                discardPile.Add(card);
            }
            
            UpdatePileCounts();
        }
        
        private void HandleEnemyDefeated()
        {
            // 检查是否通关
            if (CurrentLevel >= TotalLevels)
            {
                // 胜利！
                IsVictory = true;
                IsBattleWon = true;
                BattleStatusText = "恭喜通关！";
                return;
            }
            
            // 进入下一关
            CurrentLevel++;
            
            // 清空弃牌堆
            discardPile.Clear();
            
            InitializeLevel();
            
            // 重新初始化卡牌库
            InitializeDeck();
            DrawCards(3);
            
            BattleStatusText = $"进入第{CurrentLevel}关！";
        }
        
        [RelayCommand]
        private void EndTurn()
        {
            if (IsBattleWon || IsGameOver)
                return;
            
            // 所有活着的敌人执行意图
            foreach (var enemy in Enemies)
            {
                if (!enemy.IsAlive)
                    continue;
                    
                switch (enemy.IntentType)
                {
                    case EnemyIntentType.Attack:
                        int damageTaken = Math.Max(0, enemy.IntentValue - PlayerBlock);
                        PlayerHP = Math.Max(0, PlayerHP - damageTaken);
                        break;
                        
                    case EnemyIntentType.Defend:
                        enemy.AddBlock(enemy.IntentValue);
                        break;
                        
                    case EnemyIntentType.Buff:
                        // 蓄力效果：下次攻击伤害增加
                        enemy.IntentValue = (enemy.IntentValue * 3) / 2; // 增加50%
                        enemy.IntentText = $"猛攻 {enemy.IntentValue}";
                        break;
                }
            }
            
            // 检查玩家是否死亡
            if (PlayerHP <= 0)
            {
                IsGameOver = true;
                BattleStatusText = "游戏结束...";
                return;
            }
            
            // 回合结束重置
            PlayerBlock = 0;
            
            // 重置敌人格挡（每回合开始时清零）
            foreach (var enemy in Enemies)
            {
                enemy.Block = 0;
                enemy.GenerateIntent();
            }
            
            // 清空并重新抽牌
            HandCards.Clear();
            DrawCards(3);
            
            // 卡牌库用完重新初始化（弃牌堆洗回抽牌堆）
            if (deckCards.Count == 0)
            {
                // 将弃牌堆洗牌后移入抽牌堆
                foreach (var card in discardPile)
                {
                    deckCards.Add(card);
                }
                discardPile.Clear();
                ShuffleDeck();
                DrawCards(3);
            }
            
            UpdatePileCounts();
            
            // 更新状态
            if (string.IsNullOrEmpty(BattleStatusText) || BattleStatusText.StartsWith("进入"))
            {
                BattleStatusText = "";
            }
        }
    }
}
