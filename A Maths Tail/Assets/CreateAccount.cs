using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateAccount : MonoBehaviour {
    public Dropdown Title;
    public InputField FirstName;
    public InputField Surname;
    public InputField Email;
    public InputField Telephone;
    public InputField Username;
    public InputField Password;

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
        Debug.Log(website.text);
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
        StartCoroutine(CreateUser(title, firstname, surname, email, telephone, username, password));
    }
}
