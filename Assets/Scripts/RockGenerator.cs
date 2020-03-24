using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public int numberOfObjects = 4;
	public Vector3 startPosition;
	public Vector3 nextPosition;
    public Transform prefab;
	public float recycleOffset = 70.0f;
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
    }

	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset + 100 < character.GetComponent<CharacterController2D>().distanceTraveled) {
			Recycle();
		}
        if (character.GetComponent<CharacterController2D>().alive){
            float characterSpeed = character.GetComponent<CharacterController2D>().characterSpeed;
            rockSpeed = characterSpeed * rockParallax;
            Vector3 movement = new Vector3( rockSpeed ,  0,   0);
            //movement *= Time.deltaTime;
            //nextPosition.x += movement.x;
            movement *= Time.deltaTime;
            foreach (Transform rock in objectQueue) {
                rock.Translate(movement);
                //rock.Translate ( Vector3.Lerp(transform.position, movement, 0.5f * Time.deltaTime));
                //rockMovement(rock);
                //rock.Translate(new Vector3 (Mathf.Lerp(rock.position.x, movement.x, 0.5f * Time.deltaTime), 0, 0));
            }
            nextPosition.x += movement.x;
        }
	}

    private void rockMovement(Transform rock){
        Vector3 movement = new Vector3(0, 0, 0);
        rockSpeed = Mathf.Lerp(
            rock.position.x, 
            1, 
            Time.deltaTime * rockSpeed
        );
        rock.Translate(movement);
    }

    private void Recycle () {
        float position = nextPosition.x;
		Transform o = objectQueue.Dequeue();
        position += 0.5f * spriteSize;
		o.localPosition = new Vector3 (position, startPosition.y, o.localPosition.z);
		nextPosition.x += spriteSize;
		objectQueue.Enqueue(o);
        //Debug.Log(o.localPosition.x);
    }
}
