using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FullSerializer;
using Proyecto26;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int lastScore = -1;
    
    [SerializeField] public int scoresLoaded = 10;
    private const string _projectId = "cropodilian";
    private static readonly string _firebaseURL =
            $"https://{_projectId}-default-rtdb.europe-west1.firebasedatabase.app/";

    private static fsSerializer serializer = new fsSerializer();

    [SerializeField] private SingleScoreDisplay _scorePrefab;
    [SerializeField] private TMP_InputField _nameSubmission;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private Button _scoreButton;

    public int finalScore;

    public delegate void PostScoreCallback();
    public delegate void GetScoreCallback(Dictionary<string, ScoreModel> scores);



    
    public static void postScore(ScoreModel score, PostScoreCallback callback)
    {
        RestClient.Post<ScoreModel>($"{_firebaseURL}scores.json", score)
            .Then(response => { callback(); });
    }


    /* DOES NOT SORT LIST/DICTIONARY.
     * Sorting must be done separately.
     * However it does select the top scorers, just not in order.
     */
    public void getScores(GetScoreCallback callback)
    {

        RequestHelper request = new RequestHelper
        {
            Uri = $"{_firebaseURL}scores.json",
            Params = new Dictionary<string, string> {
                {"orderBy", "\"score\""},
                {"limitToLast", $"{scoresLoaded}"}
            },
            EnableDebug = true
        };

        RestClient.Get(request).Then(response =>
            {
                var toJson = response.Text;
                var data = fsJsonParser.Parse(toJson);
                object deserialized = null;

                serializer.TryDeserialize(data, typeof(Dictionary<string, ScoreModel>), ref deserialized);
                Dictionary<string, ScoreModel> scores = deserialized as Dictionary<string, ScoreModel>;


                // DEBUG BLOCK
                Debug.Log("Size: " + scores.Count());
                foreach (KeyValuePair<string, ScoreModel> keyValuePair in scores)
                {
                    ScoreModel value = keyValuePair.Value;
                    Debug.Log("Result: " + value.username + " with: " + value.score + " points.");
                }

                callback(scores);
            });
    }


    public void processScores(Dictionary<string, ScoreModel> dict)
    {
        List<ScoreModel> valuesList = dict.Values.ToList<ScoreModel>();
        List<ScoreModel> ordered = valuesList.OrderByDescending(val => val.score).ToList<ScoreModel>();

        int rank = 1;
        foreach (ScoreModel model in ordered)
        {
            createNewScoreElement(rank, model);
            rank++;
        }
    }


    public void updateScores()
    {
        transform.DestroyChildren();
        getScores(processScores);
    }


    public void onScoreButtonPress()
    {
        ScoreModel score = new ScoreModel();
        score.score = finalScore;
        score.username = _nameSubmission.text;
        postScore(score, updateScores);
        _scoreButton.interactable = false;
        _nameSubmission.interactable = false;
    }


    public void onMenuButtonPress()
    {
        SceneManager.LoadScene("MenuScene");
    }


    public void startNextLevel()
    {
        SeedSingleton.getInstance().randomizeSeed();
        SceneManager.LoadScene("RealGameScene");
    }

    public void restartLastLevel()
    {
        SceneManager.LoadScene("RealGameScene");
    }


    public void buttonTestScore()
    {
        ScoreModel score = new ScoreModel();
        score.score = UnityEngine.Random.Range(0, 1000000);
        postScore(score, null);
        getScores(null);

        createNewScoreElement(1, score);
    }


    public void createNewScoreElement (int rank, ScoreModel scoreModel)
    {
        SingleScoreDisplay instance = Instantiate<SingleScoreDisplay>(_scorePrefab, transform);
        instance.setValues(rank, scoreModel.score, scoreModel.username);
    }


    // Start is called before the first frame update
    void Start()
    {
        finalScore = lastScore;
        _scoreDisplay.text = finalScore.ToString();
        updateScores();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
