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

    //Start a new game
    public void NewGame() {
        SceneManager.LoadScene("Reading Puzzle 1");
    }

    //Load current progress depending on whether has saves enabled
    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("saves") != 0)
        {
            StartCoroutine(GetLevel());
        }
    }

    //Get the last level completed from the server
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
                case 4:
                    SceneManager.LoadScene("MacDonald's Farm");
                    PlayerPrefs.SetInt("score", 1000);
                    break;
                case 5:
                    SceneManager.LoadScene("Reading Puzzle 5");
                    break;
                case 6:
                    SceneManager.LoadScene("Reading Puzzle 6");
                    break;
                case 7:
                    SceneManager.LoadScene("Reading Puzzle 7");
                    break;
            }
        }
        Debug.Log(website.text);
    }
}
