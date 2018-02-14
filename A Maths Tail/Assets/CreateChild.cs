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
    string createChildURL = "https://amathstail.000webhostapp.com/CreateChild.php";
    // Use this for initialization
    void Start () {
        parentID = PlayerPrefs.GetInt("userID");
	}

    public IEnumerator CreateUser(string name, string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("parentPost", parentID);
        form.AddField("namePost", name);
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        WWW website = new WWW(createChildURL, form);
        yield return website;
        Debug.Log(website.text);
    }

    public void CreateChildUser()
    {
        string name = Name.text;
        string username = Username.text;
        string password = Password.text;
        if (name != "" && username != "" && password != "")
        {
            StartCoroutine(CreateUser(name, username, password));
        } else {
            Debug.LogWarning("Create Child Form not completed!");
        }
    }

    public void SelectChild()
    {
        SceneManager.LoadScene("Select Child");
    }
}
