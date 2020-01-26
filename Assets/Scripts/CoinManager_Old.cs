using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagerOld : MonoBehaviour {

	public GameObject coinPrefab;
	public float recycleOffset = 25.0f;
	public int numberOfObjects = 30;
	public Vector3 startPosition;
	private Vector3 nextPosition;
	public float distanceBetweenCoins = 8.0f;
	public float minX, maxX, minY, maxY;
	private GameObject[] coins;
	private List<GameObject> coinsList; 


	void Start () {
		nextPosition = startPosition;
		coinsList = new List<GameObject>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) {
			// GameObject newCoin = (GameObject)Instantiate(coinPrefab);
			// newCoin.SetActive(false);
			// coinsList.Add(newCoin);
			Recycle(i);
		}
	}
	
	void Update () {

		// float locationY = Random.Range(minY, maxY);
		// for (int i = 0; i < numberOfObjects; i++) {
		// 	coinsList[i] = (GameObject)Instantiate(coinPrefab);
		// 	coinsList[i].transform.localPosition = nextPosition;
		// 	nextPosition.x += coinsList[i].transform.localScale.x;
		// }

		for (int i = 0; i < numberOfObjects; i++) {
			if (coinsList[i].transform.localPosition.x + recycleOffset < CharacterController2D.distanceTraveled) {
				RemoveCoin(i);
				Recycle(i);
			}
		}
	}

	private void Recycle (int i) {
		//delete if: 1. out of visible range, 2. hit by character
		//add new coin?

		nextPosition.x += distanceBetweenCoins;
		GameObject newCoin = (GameObject)Instantiate(coinPrefab);
		newCoin.SetActive(true);
		float locationY = Random.Range(minY, maxY);
		newCoin.transform.localPosition = new Vector3(nextPosition.x , locationY, 0);
		coinsList.Add(newCoin);
	}

	private void RemoveCoin(int i){
		if (coinsList[i]){
			coinsList.RemoveAt(i);
		}
	}

	// void OnCollisionEnter2D(Collision2D col) 
	// {
    //     if (col.gameObject.tag == "Player")
    //     {
	// 		RemoveCoin(i);
    //     //Debug.Log("OnCollisionEnter2D");
    //     }
	// }    
}
