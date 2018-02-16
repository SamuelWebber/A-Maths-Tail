using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    public void ReplayPuzzle() {
        SceneManager.LoadScene("Level Select");
    }
	public void SignOut()
    {
        SceneManager.LoadScene("Login Menu");
    }
}
