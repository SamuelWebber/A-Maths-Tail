using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeTheTower : MonoBehaviour {
    public PuzzleToneAnalyzer toneAnalyzer;
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public Text Piece1;
    public Text Piece2;
    public Text Piece3;
    public Text Piece4;
    public Text Piece5;
    public Text Piece6;
    public Text Piece7;
    public Text Piece8;
    public Text textButton1;
    public Text textButton2;
    public GameObject Hint;
    public GameObject exitPanel;
    int timesClicked = 0;
    double wrongGuesses = 0;
    bool hintAllowed = false;
    string lasttext = "";
    int puzzleID = 1;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";

    // Use this for initialization
    void Start () {
        toneAnalyzer.StartRecording();
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

    //Get text from the button clicked, and every second click change the text to the other buttons
    public void ButtonClicked()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Text buttonText = thisButton.GetComponentInChildren<Text>();
        Debug.Log(buttonText.text);
        if (timesClicked == 0)
        {
            textButton1 = buttonText;
            timesClicked++;
            return;
        } else if (timesClicked == 1)
        {
            textButton2 = buttonText;
            string temp = textButton1.text;
            textButton1.text = textButton2.text;
            textButton2.text = temp;
            timesClicked = 0;
        }
    }

    //Check whether the text of all buttons is correct
    public void CheckCorrect()
    {
        if (Piece1.text != "10,000,000")
        {
            Piece1.color = new Color(255, 0, 0);
        }
        else
        {
            Piece1.color = new Color(0, 255, 0);
        }
        if (Piece2.text != "57,237")
        {
            Piece2.color = new Color(255, 0, 0);
        }
        else
        {
            Piece2.color = new Color(0, 255, 0);
        }
        if (Piece3.text != "4,999")
        {
            Piece3.color = new Color(255, 0, 0);
        }
        else
        {
            Piece3.color = new Color(0, 255, 0);
        }
        if (Piece4.text != "CMXCIX")
        {
            Piece4.color = new Color(255, 0, 0);
        }
        else
        {
            Piece4.color = new Color(0, 255, 0);
        }
        if (!Piece5.text.Contains("Round 493"))
        {
            Piece5.color = new Color(255, 0, 0);
        }
        else
        {
            Piece5.color = new Color(0, 255, 0);
        }
        if (Piece6.text != "13")
        {
            Piece6.color = new Color(255, 0, 0);
        }
        else
        {
            Piece6.color = new Color(0, 255, 0);
        }
        if (Piece7.text != "XII")
        {
            Piece7.color = new Color(255, 0, 0);
        }
        else
        {
            Piece7.color = new Color(0, 255, 0);
        }
        if (Piece8.text != "1")
        {
            Piece8.color = new Color(255, 0, 0);
        }
        else
        {
            Piece8.color = new Color(0, 255, 0);
        }
        if (Piece1.text == "10,000,000" && Piece2.text == "57,237" && Piece3.text == "4,999" && Piece4.text == "CMXCIX" && Piece5.text.Contains("Round 493")
            && Piece6.text == "13" && Piece7.text == "XII" && Piece8.text == "1") {
            int saves = PlayerPrefs.GetInt("saves");
            if (saves != 0)
            {
                int score = (int)(100 - (wrongGuesses / (wrongGuesses + 1)) * 100);
                StartCoroutine(UploadScore(score));
            } else {
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

    //Change the scene to the next scene in build
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Show exit panel to the user
    public void exitGame()
    {
        exitPanel.SetActive(true);
    }

    //Go back to main menu
    public void MainMenu()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene("Main Menu");
    }
}
