using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
    public InputField Username;
    public InputField Password;
    public GameObject popup;
    public Text message;
    string LoginURL = "https://amathstail.000webhostapp.com/Login.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
            SceneManager.LoadScene("Select Child");
        } else if (website.text.Contains("user")) {
            message.text = "The user with the name " + username + " does not exist!";
            showPopUp();
        } else if (website.text.Contains("password incorrect"))
        {
            message.text = "The password did not match the username!";
            showPopUp();
        }
    }

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
            showPopUp();
        }
    }

    public void CreateAccount()
    {
        SceneManager.LoadScene("Create Account");
    }

    public void showPopUp()
    {
        popup.SetActive(true);
    }

    public void hidePopUp()
    {
        popup.SetActive(false);
    }
}
