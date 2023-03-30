using GameTestApp.Helpers;

namespace GameTestApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    async void GoToTest(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameTestPage());
    }

    async void GoToScore(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScorePage());
        //File.Delete(Constants.DatabasePath);
    }
}