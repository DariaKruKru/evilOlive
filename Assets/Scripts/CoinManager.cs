using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

	public GameObject coinPrefab;
	public float recycleOffset = 35.0f;
	private float nextPosition;
	public float distanceBetweenCoins ;
	public float minX, maxX, minY, maxY;
	//private GameObject[] coins;
	private List<GameObject> coinsList; 
	private GameObject[] activeCoins;
	public float locationY;
	public GameObject character;

	void Start () {
		character = GameObject.FindWithTag("Player");
		Debug.Log("CoinManager.Start()");
		if (coinsList != null) {
			Debug.Log("coinsList.size " + coinsList.Count);
		}
	}
	
	public void GenerateCoins (float startPositionX, float endPositionX, int maxPossibleCoinsNum) {
		Debug.Log("CoinManager.GenerateCoins()");

		float platformLength = endPositionX - startPositionX;
		
		int numOfCoins = Random.Range (1, maxPossibleCoinsNum);
		float neededLength = (platformLength * numOfCoins) / maxPossibleCoinsNum;
		float maxStart = platformLength - neededLength;
		float actualStart = Random.Range(startPositionX, startPositionX + maxStart);
		nextPosition = actualStart;
		coinsList = new List<GameObject>(numOfCoins);
		for (int i = 0; i < numOfCoins; i++) {
			GameObject newCoin = (GameObject)Instantiate(coinPrefab);
			newCoin.transform.localPosition = new Vector3(nextPosition , locationY, 0);
			coinsList.Add(newCoin);
			nextPosition += distanceBetweenCoins;
		}
		Debug.Log("CoinManager.GenerateCoins coinsList.size " + coinsList.Count);
	}

	void Update () {
		activeCoins = null;
    	activeCoins = GameObject.FindGameObjectsWithTag("Bonus");
		foreach (GameObject coin in activeCoins) {
			//Debug.Log(coin.transform.localPosition.x);
			if (coin.transform.position.x + recycleOffset < character.GetComponent<CharacterController2D>().distanceTraveled) {
				//RemoveCoin(coin);
				Destroy(coin);
				//coinsList.Remove(coin);
			}
		} 
	}

	public void RemoveCoin(GameObject coin){
			coinsList.Remove(coin);
			Destroy(coin);
	}

}
