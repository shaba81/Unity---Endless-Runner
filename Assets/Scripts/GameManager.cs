using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	public PlayerController thePlayer;

	private Vector3 platformStartPoint;
	private Vector3 playerStartPoint;
	private PlatformDestroyer[] platformList;

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame(){
		StartCoroutine ("RestartGameCo");
	}

	public IEnumerator RestartGameCo(){
		thePlayer.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		platformList = FindObjectsOfType<PlatformDestroyer>();
		for(int i=0; i<platformList.Length; i++){
			platformList[i].gameObject.SetActive(false);
		}
		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive(true);
	}
}
