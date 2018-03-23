using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateChild : MonoBehaviour {
    public InputField Name;
    public InputField Username;
    public InputField Password;
    public int parentID;
    public GameObject popup;
    public Text message;
    public bool userCreated;
    string createChildURL = "https://amathstail.000webhostapp.com/CreateChild.php";
    // Use this for initialization
    void Start () {
        parentID = PlayerPrefs.GetInt("userID");
	}

    public IEnumerator CreateUser(string name, string username, string password) {
        //set up form to pass to PHP server containing values from the user interface, respond to response from PHP server
        WWWForm form = new WWWForm();
        form.AddField("parentPost", parentID);
        form.AddField("namePost", name);
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        //Pass form to PHP server
        WWW website = new WWW(createChildURL, form);
        yield return website;
        Debug.Log(website.text);
        if (website.text.Contains("exists")) {
            message.text = "Username already exists!";
            showPopUp();
        } else if (website.text.Contains("error")) {
            message.text = "Error creating user account! Please try again!";
            showPopUp();
        } else if (website.text.Contains("Successful!")) {
            message.text = "Successfully created user account!";
            userCreated = true;
            showPopUp();
        }
    }

    public void CreateChildUser()
    {
        //Get values from user interface
        string name = Name.text;
        string username = Username.text;
        string password = Password.text;
        if (name != "" && username != "" && password != "")
        {
            //Start to make call to PHP server
            StartCoroutine(CreateUser(name, username, password));
        } else {
            message.text = "Create Child Form not completed!";
            showPopUp();
        }
    }

    //Take the user to the select child menu
    public void SelectChild()
    {
        SceneManager.LoadScene("Select Child");
    }

    //Show popup to the user
    public void showPopUp() {
        popup.SetActive(true);
    }

    //Hide popup from the user
    public void hidePopUp()
    {
        popup.SetActive(false);
        if (userCreated) {
            SceneManager.LoadScene("Select Child");
        }
    }
}
