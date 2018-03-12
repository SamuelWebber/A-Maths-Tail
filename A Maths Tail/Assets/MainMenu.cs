using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    string getLevelURL = "https://amathstail.000webhostapp.com/GetLevel.php";
    // Use this for initialization
    void Start () {
		
	}
	
    //Take the user to the replay puzzle menu
    public void ReplayPuzzle() {
        SceneManager.LoadScene("Level Select");
    }

    //Take the user to the login menu
	public void SignOut()
    {
        SceneManager.LoadScene("Login Menu");
    }

    public void NewGame() {
        SceneManager.LoadScene("Reading Puzzle 1");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("saves") != 0)
        {
            StartCoroutine(GetLevel());
        }
    }

    public IEnumerator GetLevel()
    {
        WWWForm form = new WWWForm();
        form.AddField("childIDPost", PlayerPrefs.GetInt("userID"));
        WWW website = new WWW(getLevelURL,form);
        yield return website;
        Debug.Log(website.text);
        if (website.text == "" || website.text == "0")
        {
            SceneManager.LoadScene("Reading Puzzle 1");
        } else {
            int level = int.Parse(website.text);
            switch (level)
            {
                case 1:
                    SceneManager.LoadScene("Reading Puzzle 3");
                    break;
                case 2:
                    SceneManager.LoadScene("Multiplication Window");
                    break;
                case 3:
                    SceneManager.LoadScene("Reading Puzzle 4");
                    break;
            }
        }
        Debug.Log(website.text);
    }
}
