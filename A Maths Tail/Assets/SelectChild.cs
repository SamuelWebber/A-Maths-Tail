using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class SelectChild : MonoBehaviour {
    int parentID;
    public Dropdown child;
    public GameObject popup;
    public Text message;
    string GetChildrenURL = "https://amathstail.000webhostapp.com/GetChildren.php";
    List<string> names = new List<string>();
    List<int> ids = new List<int>();

	// Use this for initialization
	void Start () {
        child.ClearOptions();
        parentID = PlayerPrefs.GetInt("userID");
        StartCoroutine(GetChildren());
	}
	
    //Take user to the create child menu
	public void CreateChild()
    {
        SceneManager.LoadScene("Create Child");
    }

    //Sign the user out to login menu
    public void SignOut()
    {
        SceneManager.LoadScene("Login Menu");
    }

    //Get all children for the parentID from the server database
    public IEnumerator GetChildren()
    {
        //Get all children for the parentID
        WWWForm form = new WWWForm();
        form.AddField("parentIDPost", parentID);
        WWW website = new WWW(GetChildrenURL, form);
        yield return website;
        //If result is not empty populate the dropdown
        if (!website.text.Contains("empty")) {
            string[] users = website.text.Split(';');
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Contains(","))
                {
                    string[] field = users[i].Split(',');
                    names.Add(field[1]);
                    ids.Add(int.Parse(field[0]));
                }
            }
        }
        child.AddOptions(names);
    }

    //Select child from the drop down menu to view their scores
    public void SelectChildUser()
    {
        try
        {
            string name = child.options[child.value].text;
            int id = ids.ElementAt(child.value);
            PlayerPrefs.SetInt("ChildID", id);
            SceneManager.LoadScene("Child Score Menu");
        } catch (ArgumentOutOfRangeException) {
            message.text = "Your account does not link to any child accounts! Please create one first!";
            showPopUp();
        }
        
        
    }

    //Show popup to the user
    public void showPopUp()
    {
        popup.SetActive(true);
    }

    //Hide popup from the user
    public void hidePopUp()
    {
        popup.SetActive(false);
    }
}
