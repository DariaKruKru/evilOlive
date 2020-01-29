using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public int numberOfObjects = 3;
	public Vector3 startPosition;
	public Vector3 nextPosition;
    public Transform prefab;
	public float recycleOffset = 40.0f;
    private Queue<Transform> objectQueue;
	private float spriteSize = 3f * 33.69f;
    private float rockSpeed;
    public float rockParallax = 0.5f;
    public GameObject character;


    // Start is called before the first frame update
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
            //character = GameObject.FindWithTag("Player");
    }

	void FixedUpdate () {
		if (objectQueue.Peek().localPosition.x + recycleOffset + objectQueue.Peek().localScale.x < CharacterController2D.distanceTraveled) {
			Recycle();
		}
        float characterSpeed = character.GetComponent<CharacterController2D>().characterSpeed;
		rockSpeed = characterSpeed * rockParallax;
        foreach (Transform rock in objectQueue) {
            Vector3 movement = new Vector3( rockSpeed ,  0,   0);
			movement *= Time.deltaTime;

            rock.Translate(movement);
		}
	}

    private void Recycle () {

		float position = nextPosition.x;

		//for constant length
		position += 0.5f * spriteSize;
		Transform o = objectQueue.Dequeue();
		o.localPosition = new Vector3 (position, startPosition.y, o.localPosition.z);
		nextPosition.x += spriteSize;
		objectQueue.Enqueue(o);
    }
}
