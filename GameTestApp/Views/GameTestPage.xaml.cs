using GameTestApp.Models;
using GameTestApp.Repositories;
using GameTestApp.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GameTestApp.Views;

public partial class GameTestPage : ContentPage
{
    GameTestRepository testRepository;
    ScoreRespository scoreRespository;
    List<GameTest> gameTestList = new List<GameTest>();
    GameTest gameTestSelected = null;

    string textValue = "";
    bool radioValue = false;
    int score = 0;

    public object Player { get; private set; }

    public GameTestPage()
	{
		InitializeComponent();
        this.BindingContext = new GameTestViewModel();
        testRepository = new GameTestRepository();
        scoreRespository = new ScoreRespository();
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
        score = 0;
        GetGameTestQuestions();
    }

    void NextQuestion(object sender, EventArgs e)
    {
        if (gameTestList.Count > 1) {
            gameTestSelected = gameTestList[0];
            if (gameTestSelected.Type == "multiple")
            {
                if (radioValue)
                {
                    score++;
                    radioValue = false;
                }
            }

            if (gameTestSelected.Type == "abierta")
            {
                Debug.WriteLine(textValue);
                List<String> answers = gameTestSelected.Answer.Split(',').ToList();
                int cantAnswer = 0;
                foreach (String answer in answers)
                {
                    if (textValue.Contains(answer, StringComparison.OrdinalIgnoreCase))
                    {
                        cantAnswer++;
                    }
                }

                if (cantAnswer == answers.Count)
                {
                    score++;
                }
            }
            //----
            gameTestList.RemoveAt(0);
            ShowGameTestQuestions();
        } else
        {
            BtnFinish.IsVisible = true;
        }
    }

    async void FinishTest(object sender, EventArgs e) 
    {
        bool action = await DisplayAlert("Test Finalizado", $"Su puntaje es de { score }, ¿Quieres volver a empezar?", "Si", "No");
        if (action)
        {
            score = 0;
            GetGameTestQuestions();
        } else
        {
            scoreRespository.StoreScore(score);
            await Navigation.PopAsync();
        }
    }

    void GetEntryValue(object sender, TextChangedEventArgs e) => textValue = e.NewTextValue.ToString().ToLower();
    void GetRadioValue(object sender, CheckedChangedEventArgs e)
    { 
        RadioButton radioButton = (RadioButton)sender;
        if (e.Value)
        {
            radioValue = (bool)radioButton.Value;
        }
    }

    //----------
    async void GetGameTestQuestions()
    {
        score = 0;
        GameAnswer.Text = "";
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
        //----
        Reset();
    }

    //----------
    void Reset()
    {
        radioValue = false;
        textValue = "";
        GameAnswer.Text = "";
    }
}