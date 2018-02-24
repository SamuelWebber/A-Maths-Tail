using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    //Take the user to the replay puzzle menu
    public void ReplayPuzzle() {
        SceneManager.LoadScene("Level Select");
    }

    //Take the user to the login menu
	public void SignOut()
    {
        SceneManager.LoadScene("Login Menu");
    }

    public void NewGame() {
        SceneManager.LoadScene("Reading Puzzle 1");
    }
}
