using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
    public InputField Username;
    public InputField Password;
    public GameObject popup;
    public GameObject popup2;
    public Text message;
    string LoginURL = "https://amathstail.000webhostapp.com/Login.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Create form to pass to the server, pass credentials (username and login), server check credentials, update accordingly to the message sent back
    public IEnumerator UserLogin(string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        WWW website = new WWW(LoginURL, form);
        yield return website;
        Debug.Log(website.text);
        if (website.text.Contains("success"))
        {
            string[] fields = website.text.Split(':');
            int id = int.Parse(fields[1]);
            PlayerPrefs.SetInt("userID", id);
            if (website.text.Contains("ParentID")) {
                SceneManager.LoadScene("Select Child");
            } else if (website.text.Contains("ChildID")) {
                PlayerPrefs.SetInt("saves", 1);
                SceneManager.LoadScene("Main Menu");
            }
        } else if (website.text.Contains("user")) {
            message.text = "The user with the name " + username + " does not exist!";
            showPopUp(1);
        } else if (website.text.Contains("password incorrect"))
        {
            message.text = "The password did not match the username!";
            showPopUp(1);
        }
    }

    //Get user information from the login menu
    public void getUserInfo()
    {
        string username = Username.text;
        string password = Password.text;
        if (username != "" && password != "")
        {
            StartCoroutine(UserLogin(username, password));
        } else
        {
            message.text = "Username and password fields not completed!";
            showPopUp(1);
        }
    }

    //Take user to the create account menu
    public void CreateAccount()
    {
        SceneManager.LoadScene("Create Account");
    }

    //Show popup to the user
    public void showPopUp(int popupNumber)
    {
        if (popupNumber == 1)
        {
            popup.SetActive(true);
        } else
        {
            popup2.SetActive(true);
        }
    }

    //Hide popup from the user
    public void hidePopUp()
    {
        popup.SetActive(false);
        popup2.SetActive(false);
    }

    //Take user to the main menu
    public void SkipLogin()
    {
        if (!popup2.activeInHierarchy) {
            showPopUp(2);
        } else {
            hidePopUp();
            PlayerPrefs.SetInt("saves", 0);
            SceneManager.LoadScene("Main Menu");
        }
    }

    //Let the user exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
