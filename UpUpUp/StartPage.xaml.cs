namespace UpUpUp
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            // 导航到战斗页面
            await Shell.Current.GoToAsync("///battle");
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            // 退出应用
            Application.Current?.Quit();
        }
    }
}
