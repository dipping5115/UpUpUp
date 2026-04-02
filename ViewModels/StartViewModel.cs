namespace UpUpUp.ViewModels
{
    public class StartViewModel
    {
        public StartViewModel()
        {
        }

        public async Task StartGameAsync()
        {
            // 导航到战斗页面
            await Shell.Current.GoToAsync("battle");
        }
    }
}
