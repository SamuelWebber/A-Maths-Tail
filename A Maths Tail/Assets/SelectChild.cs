using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class SelectChild : MonoBehaviour {
    int parentID;
    public Dropdown child;
    string GetChildrenURL = "https://amathstail.000webhostapp.com/GetChildren.php";
    List<string> names = new List<string>();
    List<int> ids = new List<int>();

	// Use this for initialization
	void Start () {
        child.ClearOptions();
        parentID = PlayerPrefs.GetInt("userID");
        StartCoroutine(GetChildren());
	}
	
	public void CreateChild()
    {
        SceneManager.LoadScene("Create Child");
    }

    public void SignOut()
    {
        SceneManager.LoadScene("Login Menu");
    }

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

    public void SelectChildUser()
    {
        string name = child.options[child.value].text;
        int childID = ids.IndexOf(child.value);
        int id = ids.ElementAt(child.value);
    }
}
