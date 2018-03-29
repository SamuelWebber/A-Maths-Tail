using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public Button level7;
    string getLevelURL = "https://amathstail.000webhostapp.com/GetLevel.php";
    int level = 0;

    // Use this for initialization
    void Start () {
        //Only show replayable levels if the user has saves enabled
        if (PlayerPrefs.GetInt("saves") != 0)
        {
            StartCoroutine(GetLevel());
        }
    }
	
    //Get last completed level from the database and update level icons accordingly
	public IEnumerator GetLevel()
    {
        WWWForm form = new WWWForm();
        form.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
        WWW website = new WWW(getLevelURL, form);
        yield return website;
        Debug.Log(website.text);
        if (website.text != "" && website.text != "0")
        {
            level = int.Parse(website.text);
        }
        if (level >= 0) {
            level1.interactable = true;
        }
        if (level >= 1) {
            level2.interactable = true;
        }
        if (level >= 2) {
            level3.interactable = true;
        }
        if (level >= 3)
        {
            level4.interactable = true;
        }
        if (level >= 4)
        {
            level5.interactable = true;
        }
        if (level >= 5)
        {
            level6.interactable = true;
        }
        if (level >= 6)
        {
            level7.interactable = true;
        }
    }

    //Get the level clicked and take user to the level
    public void LoadLevel()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string levelName = thisButton.name;
        if (levelName == "Level 1")
        {
            SceneManager.LoadScene("Reading Puzzle 1");
        }
        if (levelName == "Level 2")
        {
            SceneManager.LoadScene("Reading Puzzle 3");
        }
        if (levelName == "Level 3")
        {
            SceneManager.LoadScene("Multiplication Window");
        }
        if (levelName == "Level 4")
        {
            SceneManager.LoadScene("Reading Puzzle 4");
        }
        if (levelName == "Level 5")
        {
            SceneManager.LoadScene("MacDonald's Farm");
        }
        if (levelName == "Level 6")
        {
            SceneManager.LoadScene("Reading Puzzle 5");
        }
        if (levelName == "Level 7")
        {
            SceneManager.LoadScene("Reading Puzzle 6");
        }
    }

    //Take user back to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
