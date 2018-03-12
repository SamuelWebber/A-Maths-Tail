using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public Button level1;
    public Button level2;
    public Button level3;
    string getLevelURL = "https://amathstail.000webhostapp.com/GetLevel.php";
    int level = 0;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("saves") != 0)
        {
            StartCoroutine(GetLevel());
        }
    }
	
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
        if (level >= 0)
            {
                level1.interactable = true;
            }
            if (level >= 1)
            {
                level2.interactable = true;
            }
            if (level >= 2)
        {
            level3.interactable = true;
        }
    }

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
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
