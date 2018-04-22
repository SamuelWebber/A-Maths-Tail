using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class RecipeBook : MonoBehaviour {
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject Hint;
    public GameObject exitPanel;
    public PuzzleToneAnalyzer toneAnalyzer;
    public Text value1;
    public Text value2;
    public Text value3;
    public Text value4;
    public Text value5;
    public Text value6;
    public Text value7;
    public Text value8;
    bool hintAllowed = false;
    int puzzleID = 6;
    string lasttext = "";
    double wrongGuesses = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Only update the hint button, if the last recorded tones has not been analyzed
        if (tones.text != lasttext && tones.text != "New Text")
        {
            string mainEmotion = "";
            double bestConfidence = 0;
            Debug.Log(tones.text);
            //Split the text to receive the seperate emotions
            string[] emotions = tones.text.Split(':');
            for (int i = 0; i < emotions.Length; i++)
            {
                //Parse the text to a suitable format
                emotions[i] = Regex.Replace(emotions[i], "{", "");
                emotions[i] = Regex.Replace(emotions[i], "}", "");
                //Get the seperate data from each of the emotions
                string[] emotionData = emotions[i].Split(',');
                //Get the confidence of the emotion and compare to the last confidence
                double confidence = double.Parse(emotionData[0]);
                if (confidence > bestConfidence)
                {
                    bestConfidence = confidence;
                    mainEmotion = emotionData[2];
                }
            }
            //If the overarching emotion is not Joy, then allow the user to have a hint.
            if (mainEmotion != "\"Joy\"")
            {
                hint.image.overrideSprite = hintAvailable;
                hintAllowed = true;
            }
        }
        lasttext = tones.text;
        if (wrongGuesses >= 3)
        {
            hint.image.overrideSprite = hintAvailable;
            hintAllowed = true;
        }
    }

    //Check whether values in input fields are correct, upload score if puzzle is completed, otherwise update number of wrong guesses
    public void CheckCorrect()
    {
        if ((value1.text == "1" && value2.text == "2") || (value1.text == "2" && value2.text == "1")) {
            value1.color = new Color(0, 255, 0);
            value2.color = new Color(0, 255, 0);
        } else
        {
            value1.color = new Color(255, 0, 0);
            value2.color = new Color(255, 0, 0);
        }
        if (value3.text != "20")
        {
            value3.color = new Color(255, 0, 0);
        } else
        {
            value3.color = new Color(0, 255, 0);
        }
        if (value4.text != "3")
        {
            value4.color = new Color(255, 0, 0);
        }
        else
        {
            value4.color = new Color(0, 255, 0);
        }
        if (value5.text != "3")
        {
            value5.color = new Color(255, 0, 0);
        }
        else
        {
            value5.color = new Color(0, 255, 0);
        }
        if (value6.text != "1")
        {
            value6.color = new Color(255, 0, 0);
        }
        else
        {
            value6.color = new Color(0, 255, 0);
        }
        if (value7.text != "1")
        {
            value7.color = new Color(255, 0, 0);
        }
        else
        {
            value7.color = new Color(0, 255, 0);
        }
        if (value8.text != "32")
        {
            value8.color = new Color(255, 0, 0);
        }
        else
        {
            value8.color = new Color(0, 255, 0);
        }
        if (((value1.text == "1" && value2.text == "2") || (value1.text == "2" && value2.text == "1")) && value3.text == "20" && value4.text == "3" && 
            value5.text == "3" && value6.text == "1" && value7.text == "1" && value8.text == "32")
        {
            int saves = PlayerPrefs.GetInt("saves");
            if (saves != 0)
            {
                int score = (int)(100 - (wrongGuesses / (wrongGuesses + 1)) * 100);
                StartCoroutine(UploadScore(score));
            }
            else
            {
                ChangeScene();
            }
        } else
        {
            wrongGuesses++;
        }
    }

    //Upload score to the server
    public IEnumerator UploadScore(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
        form.AddField("puzzleIDPost", puzzleID);
        WWW website = new WWW(getScoreURL, form);
        yield return website;
        Debug.Log(website.text);
        if (website.text.Contains("empty"))
        {
            WWWForm form2 = new WWWForm();
            form2.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
            form2.AddField("puzzleIDPost", puzzleID);
            form2.AddField("scorePost", score);
            WWW website2 = new WWW(insertScoreURL, form2);
            yield return website2;
            Debug.Log("inserted" + website2.text);
        }
        else
        {
            int score2 = int.Parse(website.text);
            int newScore = (score + score2) / 2;
            WWWForm form3 = new WWWForm();
            form3.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
            form3.AddField("puzzleIDPost", puzzleID);
            form3.AddField("scorePost", newScore);
            WWW website3 = new WWW(updateScoreURL, form3);
            yield return website3;
            Debug.Log("updated" + website3.text);
        }
        ChangeScene();
    }

    //Show hint panel to the user
    public void showHint()
    {
        if (hintAllowed)
        {
            Hint.SetActive(true);
        }
    }

    //Hide all panels from the user
    public void hideHint()
    {
        Hint.SetActive(false);
        exitPanel.SetActive(false);
    }

    //Show exit panel to the user
    public void exitGame()
    {
        exitPanel.SetActive(true);

    }

    //Take user to the main menu
    public void MainMenu()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene("Main Menu");
    }

    //Change to next scene in the current build
    public void ChangeScene()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
