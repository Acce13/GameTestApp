namespace GameTestApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    async void HandleNavigate(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameTestPage());
        //File.Delete(Constants.DatabasePath);
    }
}