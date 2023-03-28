using GameTestApp.Models;
using GameTestApp.Repositories;
using Newtonsoft.Json;

namespace GameTestApp.Views;

public partial class GameTestPage : ContentPage
{
    GameTestRepository testRepository;
    List<GameTest> gameTestList = new List<GameTest>();
    public GameTestPage()
	{
		InitializeComponent();
        testRepository = new GameTestRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetGameTestQuestions();
    }

    void RestartTest(object sender, EventArgs e)
    {
        GetGameTestQuestions();
    }

    void NextQuestion(object sender, EventArgs e)
    {
        DisplayAlert("Restart", "", "Ok");
    }

    async void GetGameTestQuestions()
    {
        gameTestList = await testRepository.GetGameTestsAsync();
        GameQuestion.Text = gameTestList[0].Question;
        //----
        GameAnswer.IsVisible = (gameTestList[0].Type == "abierta");
        GameOptionCollection.IsVisible = (gameTestList[0].Type == "multiple");
        //----
        GameOptionCollection.ItemsSource = JsonConvert.DeserializeObject<List<Option>>(gameTestList[0].Options);
        //----
    }
}