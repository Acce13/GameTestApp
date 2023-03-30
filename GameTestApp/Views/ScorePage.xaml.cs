using GameTestApp.Repositories;

namespace GameTestApp.Views;

public partial class ScorePage : ContentPage
{
    ScoreRespository scoreRespository;
	public ScorePage()
	{
		InitializeComponent();
        scoreRespository = new ScoreRespository();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetScores();
    }

    async void GetScores()
    {
        ScoreCollection.ItemsSource = await scoreRespository.GetScoreAsync();
    }
}