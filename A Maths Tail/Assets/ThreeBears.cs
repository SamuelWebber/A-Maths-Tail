using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ThreeBears : MonoBehaviour {
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
    int puzzleID = 7;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";
    public Sprite scarf1;
    public Sprite scarf2;
    public Sprite scarf3;
    public Sprite suitcase1;
    public Sprite suitcase2;
    public Sprite suitcase3;
    public Sprite mug1;
    public Sprite mug2;
    public Sprite mug3;
    public Sprite bottle1;
    public Sprite bottle2;
    public Sprite bottle3;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button10;
    public Button button11;
    public Button button12;
    public Sprite image1;
    public Sprite image2;
    public Sprite item1;
    public Sprite item2;
    public Sprite item3;
    public Sprite item4;
    public Sprite item5;
    public Sprite item6;
    public Sprite item7;
    public Sprite item8;
    public Sprite item9;
    public Sprite item10;
    public Sprite item11;
    public Sprite item12;
    string clickedButton1name;
    string clickedButton2name;
    bool clickedOnce = false;

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

    //Get the button which was clicked and the name, decide whether it is the first time clicked, if first set image, else if second time change the images
    //on the buttons and in the item variables
    public void ButtonClicked()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (!clickedOnce)
        {
            //Get button name and store the buttons image
            clickedButton1name = thisButton.name;
            switch (clickedButton1name)
            {
                case "Item1":
                    image1 = item1;
                    break;
                case "Item2":
                    image1 = item2;
                    break;
                case "Item3":
                    image1 = item3;
                    break;
                case "Item4":
                    image1 = item4;
                    break;
                case "Item5":
                    image1 = item5;
                    break;
                case "Item6":
                    image1 = item6;
                    break;
                case "Item7":
                    image1 = item7;
                    break;
                case "Item8":
                    image1 = item8;
                    break;
                case "Item9":
                    image1 = item9;
                    break;
                case "Item10":
                    image1 = item10;
                    break;
                case "Item11":
                    image1 = item11;
                    break;
                case "Item12":
                    image1 = item12;
                    break;
            }
            clickedOnce = !clickedOnce;
            return;
        }
        else
        {
            //Get the second buttons name and store image and swap image to first image
            clickedButton2name = thisButton.name;
            switch (clickedButton2name)
            {
                case "Item1":
                    image2 = item1;
                    button1.image.overrideSprite = image1;
                    item1 = image1;
                    break;
                case "Item2":
                    image2 = item2;
                    button2.image.overrideSprite = image1;
                    item2 = image1;
                    break;
                case "Item3":
                    image2 = item3;
                    button3.image.overrideSprite = image1;
                    item3 = image1;
                    break;
                case "Item4":
                    image2 = item4;
                    button4.image.overrideSprite = image1;
                    item4 = image1;
                    break;
                case "Item5":
                    image2 = item5;
                    button5.image.overrideSprite = image1;
                    item5 = image1;
                    break;
                case "Item6":
                    image2 = item6;
                    button6.image.overrideSprite = image1;
                    item6 = image1;
                    break;
                case "Item7":
                    image2 = item7;
                    button7.image.overrideSprite = image1;
                    item7 = image1;
                    break;
                case "Item8":
                    image2 = item8;
                    button8.image.overrideSprite = image1;
                    item8 = image1;
                    break;
                case "Item9":
                    image2 = item9;
                    button9.image.overrideSprite = image1;
                    item9 = image1;
                    break;
                case "Item10":
                    image2 = item10;
                    button10.image.overrideSprite = image1;
                    item10 = image1;
                    break;
                case "Item11":
                    image2 = item11;
                    button11.image.overrideSprite = image1;
                    item11 = image1;
                    break;
                case "Item12":
                    image2 = item12;
                    button12.image.overrideSprite = image1;
                    item12 = image1;
                    break;
            }
            //Swap the first image components for the second selected images components
            switch (clickedButton1name)
            {
                case "Item1":
                    button1.image.overrideSprite = image2;
                    item1 = image2;
                    break;
                case "Item2":
                    button2.image.overrideSprite = image2;
                    item2 = image2;
                    break;
                case "Item3":
                    button3.image.overrideSprite = image2;
                    item3 = image2;
                    break;
                case "Item4":
                    button4.image.overrideSprite = image2;
                    item4 = image2;
                    break;
                case "Item5":
                    button5.image.overrideSprite = image2;
                    item5 = image2;
                    break;
                case "Item6":
                    button6.image.overrideSprite = image2;
                    item6 = image2;
                    break;
                case "Item7":
                    button7.image.overrideSprite = image2;
                    item7 = image2;
                    break;
                case "Item8":
                    button8.image.overrideSprite = image2;
                    item8 = image2;
                    break;
                case "Item9":
                    button9.image.overrideSprite = image2;
                    item9 = image2;
                    break;
                case "Item10":
                    button10.image.overrideSprite = image2;
                    item10 = image2;
                    break;
                case "Item11":
                    button11.image.overrideSprite = image2;
                    item11 = image2;
                    break;
                case "Item12":
                    button12.image.overrideSprite = image2;
                    item12 = image2;
                    break;
            }
        }
        clickedOnce = !clickedOnce;
    }

    //Check whether the images are in the correct places
    public void CheckCorrect()
    {
        if (item1 == scarf3 && item2 == scarf1 && item3 == scarf2 && item4 == suitcase3 && item5 == suitcase1 && item6 == suitcase2 && item7 == mug1
            && item8 == mug3 && item9 == mug2 && item10 == bottle1 && item11 == bottle3 && item12 == bottle2)
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
            hintAllowed = true;
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
