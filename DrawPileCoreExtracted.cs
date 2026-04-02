        public ObservableCollection<CardModel> HandCards { get; } = new();
        private List<CardModel> deckCards = new();  // 抽牌堆
        private List<CardModel> discardPile = new();  // 弃牌堆
        
        // 抽牌堆数量
        [ObservableProperty]
        private int drawPileCount;
        
        // 弃牌堆数量
        [ObservableProperty]
        private int discardPileCount;
        
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
        
        private void UpdatePileCounts()
        {
            DrawPileCount = deckCards.Count;
            DiscardPileCount = discardPile.Count;
        }
        
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