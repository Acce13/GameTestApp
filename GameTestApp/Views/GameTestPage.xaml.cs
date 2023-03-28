using GameTestApp.Models;
using GameTestApp.Repositories;
using GameTestApp.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GameTestApp.Views;

public partial class GameTestPage : ContentPage
{
    GameTestRepository testRepository;
    List<GameTest> gameTestList = new List<GameTest>();
    GameTest gameTestSelected = null;

    string textValue = "";
    bool radioValue = false;
    int score = 0;

    public GameTestPage()
	{
		InitializeComponent();
        this.BindingContext = new GameTestViewModel();
        testRepository = new GameTestRepository();
    }

    //LifeCycles
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetGameTestQuestions();
    }

    //Element Events
    void RestartTest(object sender, EventArgs e)
    {
        GetGameTestQuestions();
    }

    void NextQuestion(object sender, EventArgs e)
    {
        if (gameTestList.Count > 1) {
            Debug.WriteLine($"TextValue {textValue}");
            Debug.WriteLine($"RadioValue {radioValue}");
            gameTestSelected = gameTestList[0];
            if (gameTestSelected.Type == "multiple")
            {

            }
            //----
            gameTestList.RemoveAt(0);
            ShowGameTestQuestions();
        } else
        {
            BtnFinish.IsVisible = true;
        }
    }

    void FinishTest(object sender, EventArgs e) 
    {
        DisplayAlert("Finish", "Has finalizado el test", "Ok");
    }

    void GetEntryValue(object sender, TextChangedEventArgs e) => textValue = e.NewTextValue.ToString().ToLower();
    void GetRadioValue(object sender, CheckedChangedEventArgs e) => radioValue = e.Value;

    //----------
    async void GetGameTestQuestions()
    {
        BtnFinish.IsVisible = false;
        gameTestList = await testRepository.GetGameTestsAsync();
        ShowGameTestQuestions();
    }

    //----------
    void ShowGameTestQuestions()
    {
        GameQuestion.Text = gameTestList[0].Question;
        //----
        GameAnswer.IsVisible = (gameTestList[0].Type == "abierta");
        GameOptionCollection.IsVisible = (gameTestList[0].Type == "multiple");
        //----
        GameOptionCollection.ItemsSource = JsonConvert.DeserializeObject<List<Option>>(gameTestList[0].Options);
    }
}