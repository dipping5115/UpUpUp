using System;
using UpUpUp.ViewModels;
using UpUpUp.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace UpUpUp
{
    public partial class MainPage : ContentPage
    {
        private CardModel? draggedCard;
        
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new BattleViewModel();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // 返回到开始页面
            await Shell.Current.GoToAsync("///start");
        }
        
        // 卡牌开始拖拽
        private void OnCardDragStarting(object sender, DragStartingEventArgs e)
        {
            if (sender is Border border && border.BindingContext is CardModel card)
            {
                draggedCard = card;
                e.Data.Properties["Card"] = card;
            }
        }
        
        // 敌人拖放 - 攻击特定敌人
        private void OnEnemyDrop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties["Card"] is CardModel card && sender is Border border)
            {
                if (border.BindingContext is EnemyModel targetEnemy)
                {
                    var viewModel = BindingContext as BattleViewModel;
                    
                    // 如果是防御卡，直接使用
                    if (card.Damage == 0)
                    {
                        viewModel?.UseDefendCard(card);
                    }
                    else
                    {
                        viewModel?.DragAttack(targetEnemy, card);
                    }
                }
            }
        }
        
        // 玩家区域拖放 - 使用防御卡
        private void OnPlayerAreaDrop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties["Card"] is CardModel card)
            {
                var viewModel = BindingContext as BattleViewModel;
                if (viewModel != null)
                {
                    // 防御卡直接使用
                    if (card.Block > 0)
                    {
                        viewModel.UseDefendCard(card);
                    }
                    // 攻击卡如果有选中的敌人则攻击
                    else if (card.Damage > 0 && viewModel.SelectedEnemy != null && viewModel.SelectedEnemy.IsAlive)
                    {
                        viewModel.DragAttack(viewModel.SelectedEnemy, card);
                    }
                }
            }
        }
    }
}
