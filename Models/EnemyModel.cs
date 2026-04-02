using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UpUpUp.Models
{
    public partial class EnemyModel : ObservableObject
    {
        [ObservableProperty]
        private string name = string.Empty;
        
        [ObservableProperty]
        private int maxHP;
        
        [ObservableProperty]
        private int currentHP;
        
        [ObservableProperty]
        private int block;
        
        // 敌人类型（影响意图生成）
        [ObservableProperty]
        private EnemyType type;
        
        // 意图
        [ObservableProperty]
        private EnemyIntentType intentType;
        
        [ObservableProperty]
        private int intentValue;
        
        [ObservableProperty]
        private string intentText = string.Empty;
        
        // 是否存活
        public bool IsAlive => CurrentHP > 0;
        
        partial void OnCurrentHPChanged(int value)
        {
            // 当HP变化时触发属性通知
            OnPropertyChanged(nameof(IsAlive));
        }
        
        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(0, damage - Block);
            CurrentHP = Math.Max(0, CurrentHP - actualDamage);
        }
        
        public void AddBlock(int block)
        {
            Block += block;
        }
        
        public void GenerateIntent()
        {
            Random random = new Random();
            
            switch (Type)
            {
                case EnemyType.Normal:
                    // 普通敌人：70%攻击 30%防御
                    if (random.Next(100) < 70)
                    {
                        IntentType = EnemyIntentType.Attack;
                        IntentValue = random.Next(6, 13);
                        IntentText = $"攻击 {IntentValue}";
                    }
                    else
                    {
                        IntentType = EnemyIntentType.Defend;
                        IntentValue = random.Next(5, 11);
                        IntentText = $"防御 +{IntentValue}";
                    }
                    break;
                    
                case EnemyType.Elite:
                    // 精英敌人：50%攻击 30%防御 20%蓄力
                    int roll = random.Next(100);
                    if (roll < 50)
                    {
                        IntentType = EnemyIntentType.Attack;
                        IntentValue = random.Next(8, 16);
                        IntentText = $"攻击 {IntentValue}";
                    }
                    else if (roll < 80)
                    {
                        IntentType = EnemyIntentType.Defend;
                        IntentValue = random.Next(8, 15);
                        IntentText = $"防御 +{IntentValue}";
                    }
                    else
                    {
                        IntentType = EnemyIntentType.Buff;
                        IntentValue = 50;
                        IntentText = "蓄力";
                    }
                    break;
                    
                case EnemyType.Boss:
                    // Boss：60%攻击 20%防御 20%强力攻击
                    int bossRoll = random.Next(100);
                    if (bossRoll < 60)
                    {
                        IntentType = EnemyIntentType.Attack;
                        IntentValue = random.Next(12, 21);
                        IntentText = $"攻击 {IntentValue}";
                    }
                    else if (bossRoll < 80)
                    {
                        IntentType = EnemyIntentType.Defend;
                        IntentValue = random.Next(10, 18);
                        IntentText = $"防御 +{IntentValue}";
                    }
                    else
                    {
                        IntentType = EnemyIntentType.Attack;
                        IntentValue = random.Next(15, 26);
                        IntentText = $"猛击 {IntentValue}";
                    }
                    break;
            }
        }
    }
    
    public enum EnemyType
    {
        Normal,  // 普通敌人
        Elite,   // 精英敌人
        Boss     // Boss
    }
    
    // 复用已有的意图类型
    public enum EnemyIntentType
    {
        Attack,
        Defend,
        Buff,
        Debuff
    }
}
