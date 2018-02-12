using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public InputField Username;
    public InputField Password;
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
    }

    public void getUserInfo()
    {
        string username = Username.text;
        string password = Password.text;
        StartCoroutine(UserLogin(username, password));
    }
}
