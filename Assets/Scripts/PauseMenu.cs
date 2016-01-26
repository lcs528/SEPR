using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class PauseMenu : MonoBehaviour {

	/// <summary>
	/// The name of the pause button in the input manager.
	/// </summary>
	public string pauseButtonName;

	/// <summary>
	/// Main pause panel
	/// </summary>
	public GameObject pauseMenuPanel;

	bool paused = false;

	// Use this for initialization
	void Start () {
		//unpause on start
		pauseMenuPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (pauseButtonName)) {
			if(!paused) {
				pauseGame();
			} else {
				unpauseGame();
			}
		}
	}


	void pauseGame() {
		//stop time, show menu and cursor
		Time.timeScale = 0;
		paused = true;
		pauseMenuPanel.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void unpauseGame () {
		//resume time, hide menu and cursor
		paused = false;
		Time.timeScale = 1;
		pauseMenuPanel.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	/*
	 * 
	 * The below methods handle each button press in the pause menu. 
	 * Theyre self explanetory.
	 * 
	 * */

	public void pressRestart () {
		Time.timeScale = 1;
		Application.LoadLevel (1);
	}

	public void pressQuitToMenu () {
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}

	public void pressExitGame () {
		Application.Quit ();
	}
}

