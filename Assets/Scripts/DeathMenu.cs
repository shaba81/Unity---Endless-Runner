using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

	public string MainMenuLevel;

	public void RestartGame(){
		FindObjectOfType<GameManager> ().Reset();
	}

	public void QuitToMain(){
		Application.LoadLevel(MainMenuLevel);
	}

}
