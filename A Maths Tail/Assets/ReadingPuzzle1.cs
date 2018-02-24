using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadingPuzzle1 : MonoBehaviour {
    public ReadingPuzzleSpeechToText speechToText;
    int wrongGuesses = 0;
    string word = "book";
    bool microphoneError = false;
    public Sprite microphoneActive;
    public Sprite microphoneInactive;
    public Button microphone;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject panel;
    public GameObject panel1;
    public Text message;
    public Text message2;
    bool isActive = false;
    bool hintAllowed = false;
    Camera maincamera;

	// Use this for initialization
	void Start () {
        //speechToText.StartRecording();
	}
	
	// Update is called once per frame
	void Update () {
        //Get the final string from IBM's Speech To Text
        string final = speechToText.getFinalText();
        //If error stop recording, show error message, set microphone button image to inactive
        if (!speechToText.Active && !microphoneError)
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
                Debug.Log("Word successfully said!");
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
    }

    //Wrong Button clicked, update wrong guesses
    public void WrongButton() {
        wrongGuesses++;
    }

    //Correct Button clicked, update database and move to next scene
    public void CorrectButton() {
        Debug.Log("Word Guessed Correctly!");
    }

    public void showHint() {
        if (wrongGuesses >= 3) {
            panel1.SetActive(true);
        }
    }
}
