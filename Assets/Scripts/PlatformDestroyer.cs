using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

	public GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {
		platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(transform.position.x < platformDestructionPoint.transform.position.x){
			//Destroy (gameObject);

			//Usiamo object pooling, le piattaforme da distruggere le rendiamo non attive
			//In modo da poterle riutilizzare
			
			gameObject.SetActive(false);
		}
	}
}
