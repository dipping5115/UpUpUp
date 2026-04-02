using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UpUpUp.Models;

namespace UpUpUp.ViewModels
{
    public partial class BattleViewModel : ObservableObject
    {
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