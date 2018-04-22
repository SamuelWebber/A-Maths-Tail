using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MacDonaldsFarm : MonoBehaviour {
    string insertScoreURL = "https://amathstail.000webhostapp.com/InsertScore.php";
    string getScoreURL = "https://amathstail.000webhostapp.com/GetScore.php";
    string updateScoreURL = "https://amathstail.000webhostapp.com/UpdateScore.php";
    int round = 1;
    int score = 0;
    public Text questiontext;
    public Image question;
    public Sprite question2;
    public Sprite question3;
    public Sprite question4;
    public Sprite question5;
    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;
    public Sprite A2;
    public Sprite B2;
    public Sprite C2;
    public Sprite D2;
    public Sprite A3;
    public Sprite B3;
    public Sprite C3;
    public Sprite D3;
    public Sprite A4;
    public Sprite B4;
    public Sprite C4;
    public Sprite D4;
    public Sprite A5;
    public Sprite B5;
    public Sprite C5;
    public Sprite D5;
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject Hint;
    public GameObject exitPanel;
    public PuzzleToneAnalyzer toneAnalyzer;
    bool hintAllowed = false;
    string lasttext = "";
    public Text scoreText;

    // Use this for initialization
    void Start () {
        int saves = PlayerPrefs.GetInt("saves");
        if (saves != 0)
        {
            int score = PlayerPrefs.GetInt("score");
            if (score <= 100)
            {
                Debug.Log(score);
                StartCoroutine(UploadScore(score, 4));
            }
        }
        
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
    }

    //Get selected answer, check if correct for current round, if so update the score accordingly. On last round send score to the server
    public void SelectedAnswer()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (round == 5)
        {
            if (name.Contains("B"))
            {
                score += 20;
            }
            StartCoroutine(UploadScore(score, 5));
        }
        if (round == 4)
        {
            question.sprite = question5;
            optionA.image.overrideSprite = A5;
            optionB.image.overrideSprite = B5;
            optionC.image.overrideSprite = C5;
            optionD.image.overrideSprite = D5;
            questiontext.text = "5. How many pigs?";
            round++;
            if (name.Contains("A"))
            {
                score += 20;
            }
            scoreText.text = "Score: " + score;
        }
        if (round == 3)
        {
            question.sprite = question4;
            optionA.image.overrideSprite = A4;
            optionB.image.overrideSprite = B4;
            optionC.image.overrideSprite = C4;
            optionD.image.overrideSprite = D4;
            questiontext.text = "4. What is the sequence?";
            round++;
            if (name.Contains("A"))
            {
                score += 20;
            }
            scoreText.text = "Score: " + score;
        }
        if (round == 2)
        {
            question.sprite = question3;
            optionA.image.overrideSprite = A3;
            optionB.image.overrideSprite = B3;
            optionC.image.overrideSprite = C3;
            optionD.image.overrideSprite = D3;
            questiontext.text = "3. How many chickens?";
            round++;
            if (name.Contains("B"))
            {
                score += 20;
            }
            scoreText.text = "Score: " + score;
        }
        if (round == 1)
        {
            question.sprite = question2;
            optionA.image.overrideSprite = A2;
            optionB.image.overrideSprite = B2;
            optionC.image.overrideSprite = C2;
            optionD.image.overrideSprite = D2;
            questiontext.text = "2. How many pigs and chickens?";
            round++;
            if (name.Contains("C"))
            {
                score += 20;
            }
            scoreText.text = "Score: " + score;
        }
    }

    //Upload score to the server to save the score
    public IEnumerator UploadScore(int score, int puzzleID)
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
        if (puzzleID == 5)
        {
            ChangeScene();
        }
    }

    //Change scene to next scene in build
    public void ChangeScene()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    //Show exit panel to the user
    public void exitGame()
    {
        exitPanel.SetActive(true);

    }

    //Go to the main menu
    public void MainMenu()
    {
        toneAnalyzer.StopRecording();
        SceneManager.LoadScene("Main Menu");
    }
}
