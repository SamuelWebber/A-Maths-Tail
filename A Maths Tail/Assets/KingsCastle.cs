using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class KingsCastle : MonoBehaviour {
    public Text question;
    public Text option1;
    public Text option2;
    public Text option3;
    public Text option4;
    public Text scoreText;
    public Image questionImage;
    public GameObject image1;
    public Image questionImage1;
    public GameObject image2;
    public Sprite question1;
    public Sprite question2;
    public Sprite question3;
    public Sprite question4;
    public Sprite question5;
    int round = 1;
    int score = 0;
    public PuzzleToneAnalyzer toneAnalyzer;
    public Text tones;
    public Sprite hintAvailable;
    public Sprite hintUnavailable;
    public Button hint;
    public GameObject Hint;
    public GameObject exitPanel;
    bool hintAllowed = false;
    string lasttext = "";
    int puzzleID = 11;
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
    }

    //Get selected answer, check if correct for current round, if so update the score accordingly. On last round send score to the server
    public void SelectedAnswer()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (round == 10)
        {
            if (name.Contains("D"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            int saves = PlayerPrefs.GetInt("saves");
            if (saves != 0)
            {
                StartCoroutine(UploadScore(score));
            }
            else
            {
                ChangeScene();
            }
        }
        if (round == 9)
        {
            question.text = "10. What is the combined age of all characters?";
            option1.text = "160";
            option2.text = "170";
            option3.text = "180";
            option4.text = "190";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 8)
        {
            question.text = "9. What is the difference in age between the Pig and the Cricket?";
            questionImage.sprite = question5;
            option1.text = "20";
            option2.text = "30";
            option3.text = "40";
            option4.text = "50";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 7)
        {
            question.text = "8. What is the total amount of money earned?";
            option1.text = "500";
            option2.text = "600";
            option3.text = "700";
            option4.text = "800";
            if (name.Contains("A"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 6)
        {
            question.text = "7. On which day did the King earn the least money?";
            image1.SetActive(true);
            image2.SetActive(false);
            questionImage.sprite = question4;
            option1.text = "Day 1";
            option2.text = "Day 2";
            option3.text = "Day 3";
            option4.text = "Day 4";
            if (name.Contains("B"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 5)
        {
            question.text = "6. Who has the most gold?";
            questionImage.sprite = question2;
            option1.text = "The Bears";
            option2.text = "Dragon";
            option3.text = "The Cricket";
            option4.text = "Sir Pig";
            if (name.Contains("B"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 4)
        {
            question.text = "5. What is the average amount of gold?";
            image1.SetActive(false);
            image2.SetActive(true);
            option1.text = "42.5";
            option2.text = "47.5";
            option3.text = "52.5";
            option4.text = "57.5";
            if (name.Contains("B"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 3)
        {
            question.text = "4. What animal has the smallest number?";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 2)
        {
            question.text = "3. Which animal has the biggest number?";
            questionImage.sprite = question2;
            option1.text = "Bears";
            option2.text = "Dragons";
            option3.text = "Crickets";
            option4.text = "Pigs";
            if (name.Contains("A"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 1)
        {
            question.text = "2. What is the difference in citizens between 2006 and 2010?";
            option1.text = "30";
            option2.text = "40";
            option3.text = "50";
            option4.text = "60";
            if (name.Contains("D"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
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
