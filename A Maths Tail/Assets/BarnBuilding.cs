using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class BarnBuilding : MonoBehaviour {
    public static int piecesRemaining = 16;
    public static double wrongGuesses = 0;
    public PuzzlePiece piece1;
    public PuzzlePiece piece2;
    public PuzzlePiece piece3;
    public PuzzlePiece piece4;
    public PuzzlePiece piece5;
    public PuzzlePiece piece6;
    public PuzzlePiece piece7;
    public PuzzlePiece piece8;
    public PuzzlePiece piece9;
    public PuzzlePiece piece10;
    public PuzzlePiece piece11;
    public PuzzlePiece piece12;
    public PuzzlePiece piece13;
    public PuzzlePiece piece14;
    public PuzzlePiece piece15;
    public PuzzlePiece piece16;
    public Text tones;
    public Button hint;
    public Sprite hintAvailable;
    public GameObject Hint;
    public GameObject exitPanel;
    public PuzzleToneAnalyzer toneAnalyzer;
    public Button upButton;
    public Button downButton;
    bool scoreSaved = false;
    bool hintAllowed = false;
    string lasttext = "";
    int puzzleID = 4;
    int index = 0;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";

    // Use this for initialization
    void Start () {
        piecesRemaining = 16;
        scoreSaved = false;
        index = 0;
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
        if (piecesRemaining == 0)
        {
            int saves = PlayerPrefs.GetInt("saves");
            if (saves != 0 && !scoreSaved)
            {
                toneAnalyzer.StopRecording();
                Debug.Log("score saved");
                int score = (int)((16 / (16 + wrongGuesses)) * 100);
                PlayerPrefs.SetInt("score", score);
                scoreSaved = true;
            }
            else
            {
                ChangeScene();
            }
            
        }
	}

    //If up is clicked move pieces up
    public void UpArrow()
    {
        index++;
        downButton.interactable = true;
        if (index == 6)
        {
            upButton.interactable = false;
        }
        piece1.MovePiece(true);
        piece2.MovePiece(true);
        piece3.MovePiece(true);
        piece4.MovePiece(true);
        piece5.MovePiece(true);
        piece6.MovePiece(true);
        piece7.MovePiece(true);
        piece8.MovePiece(true);
        piece9.MovePiece(true);
        piece10.MovePiece(true);
        piece11.MovePiece(true);
        piece12.MovePiece(true);
        piece13.MovePiece(true);
        piece14.MovePiece(true);
        piece15.MovePiece(true);
        piece16.MovePiece(true);
    }

    //If down is clicked move pieces down
    public void DownArrow()
    {
        index--;
        if (index == -6)
        {
            downButton.interactable = false;
        }
        upButton.interactable = true;
        piece1.MovePiece(false);
        piece2.MovePiece(false);
        piece3.MovePiece(false);
        piece4.MovePiece(false);
        piece5.MovePiece(false);
        piece6.MovePiece(false);
        piece7.MovePiece(false);
        piece8.MovePiece(false);
        piece9.MovePiece(false);
        piece10.MovePiece(false);
        piece11.MovePiece(false);
        piece12.MovePiece(false);
        piece13.MovePiece(false);
        piece14.MovePiece(false);
        piece15.MovePiece(false);
        piece16.MovePiece(false);
    }

    //Upload score to the database
    public IEnumerator UploadScore(int score)
    {
        Debug.Log("hello");
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

    //show hint to the user if allowed
    public void showHint()
    {
        if (hintAllowed)
        {
            Hint.SetActive(true);
        }
    }

    //Hide panels from the user
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

    //Show exit panel to the user
    public void exitGame()
    {
        exitPanel.SetActive(true);
    }

    //Take the user back to the main menu
    public void MainMenu()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene("Main Menu");
    }
}
