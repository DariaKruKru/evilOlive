using System.Collections.Generic;
using UnityEngine;

public class SpeedLinesGenerator : MonoBehaviour
{
    
    public int numberOfObjects = 10;
	public Vector3 startPosition;
	public Vector3 nextPosition;
    public int numOfPrefabs = 5;
    public Transform[] prefab;
	public float recycleOffset = 70.0f;
    private Queue<Transform> objectQueue;
	private float spriteSize = 5f;
    public float minSize, maxSize, minGapX, maxGapX, minGapY, maxGapY, minPosY, maxPosY;
     private float lineSpeed;
    public float lineParallax = 0.18f;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        	objectQueue = new Queue<Transform>(numberOfObjects);
			nextPosition = startPosition;
            
			for (int i = 0; i < numberOfObjects; i++) {
                int j = Random.Range(0, numOfPrefabs);
				objectQueue.Enqueue((Transform)Instantiate(prefab[j]));
			}

			for (int i = 0; i < numberOfObjects; i++) {
				Recycle();
			}
    }

	void FixedUpdate () {
		if (objectQueue.Peek().localPosition.x + recycleOffset + objectQueue.Peek().localScale.x < CharacterController2D.distanceTraveled) {
			Recycle();
		}
        float characterSpeed = character.GetComponent<CharacterController2D>().characterSpeed;
		lineSpeed = characterSpeed * lineParallax;
        foreach (Transform rock in objectQueue) {
            Vector3 movement = new Vector3( lineSpeed ,  0,   0);
			movement *= Time.deltaTime;

            rock.Translate(movement);
		}
	}

private void Recycle () {

		Vector3 position = nextPosition;
		Transform o = objectQueue.Dequeue();

		//for constant length
		// position += 0.5f * spriteSize;
		// Transform o = objectQueue.Dequeue();
		// o.localPosition = new Vector3 (position, o.localPosition.y, o.localPosition.z);
		// nextPosition.x += spriteSize;
		// objectQueue.Enqueue(o);

		//for random length
		float scaleX = Random.Range(minSize, maxSize);
        float scaleY = Random.Range(minSize, maxSize);
		Vector3 currentSpriteSize = new Vector3 (spriteSize * scaleX, spriteSize * scaleY, -2);
		position.x += 0.5f * currentSpriteSize.x;
        position.y = Random.Range(minPosY, maxPosY);
	
		o.localScale = new Vector3 (scaleX, scaleY, -2 );
		o.localPosition = new Vector3 (position.x, position.y, -2);

		//Debug.Log(maxPossibleCoinsNum);
		Vector3 gap = new Vector3 (Random.Range(minGapX, maxGapX), Random.Range(minGapY, maxGapY), -2);
		nextPosition += currentSpriteSize + gap;
		objectQueue.Enqueue(o);

	}
}
