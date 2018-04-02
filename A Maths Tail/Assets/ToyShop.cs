using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ToyShop : MonoBehaviour {
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
    public Image questionImage2;
    public GameObject image3;
    public Sprite question2;
    public Sprite question3;
    public Sprite question4;
    public Sprite question5;
    public Sprite question6;
    public Sprite question7;
    public Sprite question8;
    public Sprite question9;
    int round = 1;
    int score = 0;
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
    int puzzleID = 10;
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
            if (name.Contains("B"))
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
            question.text = "10. Which plane should the shape be in, if rotated 90° clockwise around the origin?";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 8)
        {
            question.text = "9. Which plane should the shape be in if reflected through the origin?";
            image3.SetActive(false);
            image1.SetActive(true);
            questionImage.sprite = question9;
            option1.text = "plane 1";
            option2.text = "plane 2";
            option3.text = "plane 3";
            option4.text = "plane 4";
            if (name.Contains("D"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 7)
        {
            question.text = "8. What is the area of this shape?";
            image1.SetActive(false);
            image3.SetActive(true);
            option1.text = "60";
            option2.text = "65";
            option3.text = "70";
            option4.text = "75";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 6)
        {
            question.text = "7. What is the perimeter of this shape?";
            questionImage.sprite = question7;
            option1.text = "10";
            option2.text = "15";
            option3.text = "20";
            option4.text = "25";
            if (name.Contains("D"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 5)
        {
            question.text = "6. What is the sum of all angles in this shape?";
            questionImage.sprite = question6;
            image2.SetActive(false);
            image1.SetActive(true);
            questionImage.sprite = question6;
            option1.text = "90°";
            option2.text = "180°";
            option3.text = "270°";
            option4.text = "360°";
            if (name.Contains("B"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 4)
        {
            question.text = "5. What is the value of angle X?";
            image1.SetActive(false);
            image2.SetActive(true);
            option1.text = "30°";
            option2.text = "45°";
            option3.text = "60°";
            option4.text = "90°";
            if (name.Contains("B"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 3)
        {
            question.text = "4. Which family does this shape belong to?";
            questionImage.sprite = question4;
            option1.text = "Rhombi";
            option2.text = "Trapezoids";
            option3.text = "Triangles";
            option4.text = "Ellipses";
            if (name.Contains("D"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 2)
        {
            question.text = "3. Which part of the circle has been mislabelled?";
            questionImage.sprite = question3;
            option1.text = "Radius";
            option2.text = "Diameter";
            option3.text = "Circumference";
            option4.text = "None of the above";
            if (name.Contains("C"))
            {
                score += 10;
            }
            scoreText.text = "Score: " + score;
            round++;
        }
        if (round == 1)
        {
            question.text = "2. Which shape is this?";
            questionImage.sprite = question2;
            option1.text = "Cuboid";
            option2.text = "Sphere";
            option3.text = "Cylinder";
            option4.text = "Pyramid";
            if (name.Contains("A"))
            {
                score += 10;
            }
            scoreText.text = "Score: "+score;
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
