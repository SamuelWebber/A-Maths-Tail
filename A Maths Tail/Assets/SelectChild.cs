using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectChild : MonoBehaviour {
    public int userID;
    public Dropdown child;

	// Use this for initialization
	void Start () {
        child.ClearOptions();
	}
	
	public void CreateChild()
    {
        SceneManager.LoadScene("Create Child");
    }
}
