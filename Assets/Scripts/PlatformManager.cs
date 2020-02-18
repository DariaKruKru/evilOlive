using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public int numberOfObjects = 10;
	public Vector3 startPosition;
	private Vector3 nextPosition;
	private float previousSize;
	public Transform prefab;
	public float recycleOffset = 40.0f;
	private Queue<Transform> objectQueue;
	private float spriteSize = 2*4.67f;
	public float minSize, maxSize, minGap, maxGap;
	private float maxDistance = 1;
	public GameObject coinGenerator;
	public GameObject coinGenerator_second;

	void OnEnable() 
		{
			objectQueue = new Queue<Transform>(numberOfObjects);
			nextPosition = startPosition;
			for (int i = 0; i < numberOfObjects; i++) {
				objectQueue.Enqueue((Transform)Instantiate(prefab));
			}
			for (int i = 0; i < numberOfObjects; i++) {
				Recycle();
			}
		}

	void FixedUpdate () {
		if (objectQueue.Peek().localPosition.x + recycleOffset + objectQueue.Peek().localScale.x < CharacterController2D.distanceTraveled) {
			Recycle();
		}
	}

	private void Recycle () {

		float position = nextPosition.x;
		
		//for constant length
		// position += 0.5f * spriteSize;
		// Transform o = objectQueue.Dequeue();
		// o.localPosition = new Vector3 (position, o.localPosition.y, o.localPosition.z);
		// nextPosition.x += spriteSize;
		// objectQueue.Enqueue(o);

		//for random length
		float scale = Random.Range(minSize, maxSize);
		float currentSpriteSize = spriteSize * scale;
		position += 0.5f * currentSpriteSize;
		Transform o = objectQueue.Dequeue();

		int maxPossibleCoinsNum = System.Convert.ToInt32( System.Math.Truncate ((currentSpriteSize)/coinGenerator.GetComponent<CoinManager>().distanceBetweenCoins));
		coinGenerator.GetComponent<CoinManager>().GenerateCoins(nextPosition.x, nextPosition.x + currentSpriteSize , maxPossibleCoinsNum);

		if (coinGenerator_second){
			coinGenerator_second.GetComponent<CoinManager>().GenerateCoins(nextPosition.x, nextPosition.x + currentSpriteSize , maxPossibleCoinsNum);
		}

		o.localScale = new Vector3 (scale * 2 , o.localScale.y, o.localScale.z );
		o.localPosition = new Vector3 (position, nextPosition.y, o.localPosition.z);

		//Debug.Log(maxPossibleCoinsNum);
		float gap = Random.Range(minGap, maxGap);
		nextPosition.x += currentSpriteSize + gap;
		objectQueue.Enqueue(o);

	}
}