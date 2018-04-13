using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MemoryGame : MonoBehaviour {
    public PuzzleToneAnalyzer toneAnalyzer;
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject Hint;
    public GameObject exitPanel;
    bool hintAllowed = false;
    string lasttext = "";
    int puzzleID = 8;
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";
    public Button memoryCard1;
    public Button memoryCard2;
    public Button memoryCard3;
    public Button memoryCard4;
    public Button memoryCard5;
    public Button memoryCard6;
    public Button memoryCard7;
    public Button memoryCard8;
    public Button memoryCard9;
    public Button memoryCard10;
    public Button memoryCard11;
    public Button memoryCard12;
    public Button memoryCard13;
    public Button memoryCard14;
    public Button memoryCard15;
    public Button memoryCard16;
    public Sprite A1;
    public Sprite B1;
    public Sprite A2;
    public Sprite B2;
    public Sprite A3;
    public Sprite B3;
    public Sprite A4;
    public Sprite B4;
    public Sprite A5;
    public Sprite B5;
    public Sprite A6;
    public Sprite B6;
    public Sprite A7;
    public Sprite B7;
    public Sprite A8;
    public Sprite B8;
    public Sprite backCover;
    Button firstCard;
    bool clickedOnce;
    int pairsFound = 0;
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

    //If card is clicked, find the card which was clicked and show the image.
    public void CardClicked()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string clickedButtonName = thisButton.name;
        Button button = null;
        switch (clickedButtonName)
        {
            case "Memory Card 1":
                memoryCard1.image.overrideSprite = B1;
                button = memoryCard1;
                break;
            case "Memory Card 2":
                memoryCard2.image.overrideSprite = A4;
                button = memoryCard2;
                break;
            case "Memory Card 3":
                memoryCard3.image.overrideSprite = A6;
                button = memoryCard3;
                break;
            case "Memory Card 4":
                memoryCard4.image.overrideSprite = A2;
                button = memoryCard4;
                break;
            case "Memory Card 5":
                memoryCard5.image.overrideSprite = B6;
                button = memoryCard5;
                break;
            case "Memory Card 6":
                memoryCard6.image.overrideSprite = A3;
                button = memoryCard6;
                break;
            case "Memory Card 7":
                memoryCard7.image.overrideSprite = B7;
                button = memoryCard7;
                break;
            case "Memory Card 8":
                memoryCard8.image.overrideSprite = A5;
                button = memoryCard8;
                break;
            case "Memory Card 9":
                memoryCard9.image.overrideSprite = B5;
                button = memoryCard9;
                break;
            case "Memory Card 10":
                memoryCard10.image.overrideSprite = A8;
                button = memoryCard10;
                break;
            case "Memory Card 11":
                memoryCard11.image.overrideSprite = A1;
                button = memoryCard11;
                break;
            case "Memory Card 12":
                memoryCard12.image.overrideSprite = A7;
                button = memoryCard12;
                break;
            case "Memory Card 13":
                memoryCard13.image.overrideSprite = B2;
                button = memoryCard13;
                break;
            case "Memory Card 14":
                memoryCard14.image.overrideSprite = B8;
                button = memoryCard14;
                break;
            case "Memory Card 15":
                memoryCard15.image.overrideSprite = B4;
                button = memoryCard15;
                break;
            case "Memory Card 16":
                memoryCard16.image.overrideSprite = B3;
                button = memoryCard16;
                break;
        }
        //If this is the first click, then save current button and set trigger for clicks
        if (!clickedOnce)
        {
            firstCard = button;
            clickedOnce = true;
        } else {
            //Else if second click, check whether a pair has been found, and if so update the amount of pairs found
            if (((firstCard.tag == "pair1" && button.tag == "pair1") || (firstCard.tag == "pair2" && button.tag == "pair2") || 
                (firstCard.tag == "pair3" && button.tag == "pair3") || (firstCard.tag == "pair4" && button.tag == "pair4") ||
                (firstCard.tag == "pair5" && button.tag == "pair5") || (firstCard.tag == "pair6" && button.tag == "pair6") ||
                (firstCard.tag == "pair7" && button.tag == "pair7") || (firstCard.tag == "pair8" && button.tag == "pair8")) &&
                firstCard != button)
            {
                GameObject card1 = GameObject.Find(firstCard.name);
                StartCoroutine(hideCards(card1, thisButton));
                pairsFound++;
                //If all pairs have been found, save score and finish game
                if (pairsFound == 8)
                {
                    int saves = PlayerPrefs.GetInt("saves");
                    if (saves != 0)
                    {
                        int score = (int)(100 -(wrongGuesses / (wrongGuesses + 8)) * 100);
                        StartCoroutine(UploadScore(score));
                    }
                    else
                    {
                        ChangeScene();
                    }
                }
            } else
            {
                //If the pair do not match, then hide images and increment number of wrong guesses
                StartCoroutine(showCovers(button));
                wrongGuesses++;
            }
            clickedOnce = false;
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

    //Wait for a second, then turn card image back to the back covers of the cards
    public IEnumerator showCovers(Button button)
    {
        yield return new WaitForSeconds(1);
        firstCard.image.overrideSprite = backCover;
        button.image.overrideSprite = backCover;
    }

    //Wait for a second, then hide the cards from the game board
    public IEnumerator hideCards(GameObject card1, GameObject card2)
    {
        yield return new WaitForSeconds(1);
        card1.SetActive(false);
        card2.SetActive(false);
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
