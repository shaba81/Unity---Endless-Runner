using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	
	public Transform generationPoint;
	
	public float distanceBetween;
	public float distanceBetweenMin;
	public float distanceBetweenMax;
	public float maxHeightChange;
	
	public ObjectPooler[] theObjectPools;

	public Transform maxHeightPoint;

	public float randomCoinThreshold;

	private float platformWidth;
	private int platformSelector;
	private float[] platformWidths;
	
	private float minHeight;
	private float maxHeight;
	private float heightChange;

	private CoinGenerator theCoinGenerator;
	

	// Use this for initialization
	void Start () {
		platformWidths = new float[theObjectPools.Length];

		for(int i=0; i<theObjectPools.Length; i++){
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(transform.position.x < generationPoint.position.x){
			
			distanceBetween = Random.Range(distanceBetweenMin,distanceBetweenMax);
			
			platformSelector = Random.Range(0,theObjectPools.Length);

			heightChange = transform.position.y + Random.Range(maxHeightChange,-maxHeightChange);

			if(heightChange > maxHeight){
				heightChange = maxHeight;
			}
			else if (heightChange < minHeight){
				heightChange = minHeight;
			}
			
			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);
		
			//Crea una nuova piattaforma
			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			if(Random.Range(0f,100f) < randomCoinThreshold)
				theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.z);
		}
			
	}
}
