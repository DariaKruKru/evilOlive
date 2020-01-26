using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	float offset;

	void Start()
	{
		offset = transform.position.x - target.position.x;
	}

	void FixedUpdate()
	{
		float targetCamPos = target.position.x + offset;
		transform.position = new Vector3(Mathf.Lerp (transform.position.x, targetCamPos, smoothing * Time.deltaTime), transform.position.y, transform.position.z);
	}
}
