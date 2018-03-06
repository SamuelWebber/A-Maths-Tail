using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public Button level1;
    string getLevelURL = "https://amathstail.000webhostapp.com/GetLevel.php";
    int level = 0;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("saves") != 0)
        {
            StartCoroutine(GetLevel());
            if (level >= 0)
            {
                level1.interactable = true;
            }
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
        Debug.Log(website.text);
    }

    public void LoadLevel()
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string levelName = thisButton.name;
        if (levelName == "Level 1")
        {
            SceneManager.LoadScene("Reading Puzzle 1");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
