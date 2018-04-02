using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DragonsLair : MonoBehaviour {
    public Text changeText;
    bool coin1 = false;
    bool coin2 = false;
    bool coin3 = false;
    bool coin4 = false;
    bool coin5 = false;
    bool coin6 = false;
    bool coin7 = false;
    bool coin8 = false;
    bool coin9 = false;
    double change = 0.00;
    public PuzzleToneAnalyzer toneAnalyzer;
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject Hint;
    public GameObject exitPanel;
    double wrongGuesses = 0;
    bool hintAllowed = false;
    string lasttext = "";
    int puzzleID = 9;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";

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
        changeText.text = "Current Change: £" + change;
	}

    //If coin is clicked, check which coin was clicked and update accordingly
    public void CalculateChange()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (thisButton.name == "£1") {
            if (coin1) {
                change -= 1;
                coin1 = false;
            } else {
                change += 1;
                coin1 = true;
            }
        }
        if (thisButton.name == "2p") {
            if (coin2) {
                change -= 0.02;
                coin2 = false;
            } else {
                change += 0.02;
                coin2 = true;
            }
        }
        if (thisButton.name == "1p") {
            if (coin3) {
                change -= 0.01;
                coin3 = false;
            } else {
                change += 0.01;
                coin3 = true;
            }
        }
        if (thisButton.name == "20p") {
            if (coin4) {
                change -= 0.2;
                coin4 = false;
            } else {
                change += 0.2;
                coin4 = true;
            }
        }
        if (thisButton.name == "10p") {
            if (coin5) {
                change -= 0.1;
                coin5 = false;
            } else {
                change += 0.1;
                coin5 = true;
            }
        }
        if (thisButton.name == "5p") {
            if (coin6) {
                change -= 0.05;
                coin6 = false;
            } else {
                change += 0.05;
                coin6 = true;
            }
        }
        if (thisButton.name == "50p") {
            if (coin7) {
                change -= 0.5;
                coin7 = false;
            } else {
                change += 0.5;
                coin7 = true;
            }
        }
        if (thisButton.name == "10p2") {
            if (coin8) {
                change -= 0.1;
                coin8 = false;
            } else {
                change += 0.1;
                coin8 = true;
            }
        }
        if (thisButton.name == "£2")
        {
            if (coin9) {
                change -= 2;
                coin9 = false;
            } else {
                change += 2;
                coin9 = true;
            }
        }
    }

    //Check the total value of change, if correct then save score, otherwise update wrong guesses
    public void CheckCorrect()
    {
        if ((change + "") == "1.98") {
            int score = (int)(100 - (wrongGuesses / (wrongGuesses + 1)) * 100);
            int saves = PlayerPrefs.GetInt("saves");
            if (saves != 0)
            {
                StartCoroutine(UploadScore(score));
            } else
            {
                ChangeScene();
            }
        } else {
            wrongGuesses++;
            Debug.Log(wrongGuesses);
        }
    }

    //Upload score to the database
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
        toneAnalyzer.StopRecording();
        ChangeScene();
    }

    //Show hint to the user if allowed
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

    //Change scene to next scene in build
    public void ChangeScene()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Show exit panel to user
    public void exitGame()
    {
        exitPanel.SetActive(true);
    }

    //Take user back to the main menu
    public void MainMenu()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene("Main Menu");
    }
}
