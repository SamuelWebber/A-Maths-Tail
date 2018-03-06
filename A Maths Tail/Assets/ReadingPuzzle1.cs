using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadingPuzzle1 : MonoBehaviour {
    public ReadingPuzzleSpeechToText speechToText;
    double wrongGuesses = 0;
    public string word;
    int puzzleID = 12;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";
    bool microphoneError = false;
    public Sprite microphoneActive;
    public Sprite microphoneInactive;
    public Button microphone;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject panel;
    public GameObject panel1;
    public GameObject panel2;
    public Text message;
    bool isActive = false;
    bool hintAllowed = false;
    Camera maincamera;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Get the final string from IBM's Speech To Text
        string final = speechToText.getFinalText();
        //If error stop recording, show error message, set microphone button image to inactive
        if (!speechToText.active && !microphoneError)
        {
            message.text = "There is an error with the Microphone! Please use the buttons!";
            panel.SetActive(true);
            microphone.image.overrideSprite = microphoneInactive;
            microphoneError = true;
            speechToText.StopRecording();
        }
        //If final is not empty, then check the guessed word to see if it is correct and update accordingly
        if (final != "")
        {
            speechToText.StopRecording();
            microphone.image.overrideSprite = microphoneInactive;
            isActive = false;
            if (final.Contains(word)) {
                WordGuessed();
            } else {
                wrongGuesses++;
            }
        }
        if (wrongGuesses >= 3) {
            hint.image.overrideSprite = hintAvailable;
        }
	}

    //Record the user's voice to obtain there guess
    public void recordVoice()
    {
        speechToText.StartRecording();
        if (microphoneError) {
            panel.SetActive(true);
        } else {
            if (!isActive)
            {
                microphone.image.overrideSprite = microphoneActive;
                isActive = true;
            }
            else
            {
                microphone.image.overrideSprite = microphoneInactive;
                isActive = false;
            }
        }
    }

    //Hide error message
    public void hidePanel() {
        panel.SetActive(false);
        panel1.SetActive(false);
        panel2.SetActive(false);
    }

    //Wrong Button clicked, update wrong guesses
    public void WrongButton() {
        wrongGuesses++;
    }

    //Correct Button clicked, update database and move to next scene
    public void CorrectButton() {
        WordGuessed();
    }

    //Show user a hint, if they have got 3 guesses wrong!
    public void showHint() {
        if (wrongGuesses >= 3) {
            panel1.SetActive(true);
        }
    }

    //The user has correctly guessed the word, upload score to database and move to next scene
    public void WordGuessed() {
        speechToText.StopRecording();
        int saves = PlayerPrefs.GetInt("saves");
        if (saves != 0) {
            int score = (int)(100 - (wrongGuesses / (wrongGuesses + 1)) * 100);
            StartCoroutine(UploadScore(score));
        } else {
            ChangeScene();
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
        if (website.text.Contains("empty")) {
            WWWForm form2 = new WWWForm();
            form2.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
            form2.AddField("puzzleIDPost", puzzleID);
            form2.AddField("scorePost", score);
            WWW website2 = new WWW(insertScoreURL, form2);
            yield return website2;
            Debug.Log(website2.text);
        } else {
            int score2 = int.Parse(website.text);
            int newScore = (score + score2)/2;
            WWWForm form3 = new WWWForm();
            form3.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
            form3.AddField("puzzleIDPost", puzzleID);
            form3.AddField("scorePost", newScore);
            WWW website3 = new WWW(updateScoreURL, form3);
            yield return website3;
            Debug.Log(website3.text);
        }
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exitGame()
    {
        panel2.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
