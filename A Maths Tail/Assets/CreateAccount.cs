using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CreateAccount : MonoBehaviour {
    public Dropdown Title;
    public InputField FirstName;
    public InputField Surname;
    public InputField Email;
    public InputField Telephone;
    public InputField Username;
    public InputField Password;
    public GameObject popup;
    public Text message;
    private bool userCreated = false;

    string createAccountURL = "https://amathstail.000webhostapp.com/CreateAccount.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame

    public IEnumerator CreateUser(string title, string firstname, string surname, string email, string telephone, string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("titlePost",title);
        form.AddField("firstnamePost", firstname);
        form.AddField("surnamePost", surname);
        form.AddField("emailPost", email);
        form.AddField("telephonePost", telephone);
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        WWW website = new WWW(createAccountURL,form);
        yield return website;
        if (website.text.Contains("exists")) {
            message.text = "Username Already Exists!";
            showPopUp();
        } else if (website.text.Contains("error")) {
            message.text = "Error creating user account! Please try again!";
            showPopUp();
        } else if (website.text.Contains("Successful")) {
            message.text = "Successfully Created User Account!";
            showPopUp();
            userCreated = true;
        }
    }

    public void CreateAccountUser()
    {
        string title = Title.options[Title.value].text;
        string firstname = FirstName.text;
        string surname = Surname.text;
        string email = Email.text;
        string telephone = Telephone.text;
        string username = Username.text;
        string password = Password.text;
        if (title != "" && firstname != "" && surname != "" && email != "" && telephone != "" && username != "" && password != "")
        {
            if (email.Contains("@") && email.Contains("."))
            {
                StartCoroutine(CreateUser(title, firstname, surname, email, telephone, username, password));
            } else {
                message.text = "Email is incorrect!";
                showPopUp();
                return;
            }
        } else {
            message.text = "Registration form was not completed!";
            showPopUp();
        }
    }

    public void showPopUp()
    {
        popup.SetActive(true);
    }

    public void hidePopUp()
    {
        popup.SetActive(false);
        if (userCreated) {
            SceneManager.LoadScene("Login Menu");
        }
    }
}
