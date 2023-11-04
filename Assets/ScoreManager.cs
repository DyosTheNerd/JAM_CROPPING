using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FullSerializer;
using Proyecto26;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    [SerializeField] public int scoresLoaded = 10;
    private const string _projectId = "cropodilian";
    private static readonly string _firebaseURL =
            $"https://{_projectId}-default-rtdb.europe-west1.firebasedatabase.app/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostScoreCallback();
    public delegate void GetScoreCallback(Dictionary<string, ScoreModel> scores);


    public static void postScore(ScoreModel score, PostScoreCallback callback)
    {
        RestClient.Post<ScoreModel>($"{_firebaseURL}scores.json", score)
            .Then(response => { callback(); });
    }


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


    public void buttonTestScore()
    {
        ScoreModel score = new ScoreModel();
        score.score = UnityEngine.Random.Range(0, 1000000);
        postScore(score, null);
        Debug.Log("Score loading.");
        getScores(null);
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
