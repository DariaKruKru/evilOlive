using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    
	public Vector3 startPosition;
	public Vector3 nextPosition;
    public int numberOfObjects = 10;
    public Transform prefab;
	public float recycleOffset = 60.0f;
    private Queue<Transform> objectQueue;
    public float minGapX, maxGapX, minPosY = -4.2f, maxPosY= 4.2f;

    void Start()
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
        Vector3 position = nextPosition;
        position.y = Random.Range(minPosY, maxPosY);
		Transform o = objectQueue.Dequeue();
		o.localPosition = new Vector3 (position.x, position.y, 0);
		objectQueue.Enqueue(o);
        float gap = Random.Range(minGapX, maxGapX);
        nextPosition.x += gap;
        }

	}
