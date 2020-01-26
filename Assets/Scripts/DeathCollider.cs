using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {

	// Use this for initialization
	public Transform target;
	float offset;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float targetColliderPos = target.position.x;
		transform.position = new Vector3(targetColliderPos, transform.position.y, transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		

	} 
}
