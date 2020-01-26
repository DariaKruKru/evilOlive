﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CircleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    public float characterSpeed = 12f;
    private bool grounded;
    public float speedMilestoun;
    public float speedInterval;
    public float speedIncreaser;
    public float gravityIncreaser;
    public float smoothSpeedUpCoeff = 4;
    public static float distanceTraveled;
    public bool downGravity = true;
    public CircleCollider2D circleCollider;
    public Rigidbody2D rigidbody;
    public TrailRenderer trail;
    private float gravityScale = 10f;
    public static int lives = 3;
    public bool isGrounded = false;
    public static int score = 0;
    private float waitingTime = 1.5f;

    private bool alive = true;

    private void Awake()
    {      
        circleCollider = GetComponent<CircleCollider2D>();
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (alive){
            Movement(); 

            if (Input.GetKeyDown("space") && isGrounded)
            {
                ChangeGravity();
            }
        }     
    }  

	private void Movement()
	{
        //speed increasing as far player move
        if (transform.localPosition.x > speedMilestoun){
            //speedMilestoun += speedInterval;
            speedMilestoun += speedMilestoun;
            characterSpeed += speedIncreaser;
            gravityScale += gravityIncreaser; //vertical speed
        }

        Vector2 newVelocity = rigidbody.velocity;
        newVelocity.x = Mathf.Lerp(
            newVelocity.x, 
            characterSpeed, 
            Time.deltaTime * smoothSpeedUpCoeff
        );

        rigidbody.velocity = newVelocity;
        distanceTraveled = (float)System.Math.Round(transform.localPosition.x, 0); 

        //Debug.Log(distanceTraveled);
	}

    private void ChangeGravity()
    {
        if (downGravity == false){
            rigidbody.gravityScale = gravityScale;
            downGravity = true;
        } else {
            rigidbody.gravityScale = -gravityScale;
            downGravity = false;
        }
        //animation of rotation

        //flip character
        //rigidbody.transform.Rotate(new Vector3(rigidbody.transform.rotation.x + 180f, 0, 0));
    }

	void OnCollisionEnter2D(Collision2D col) 
	{
        if (col.gameObject.tag == "Platform")
        {
		isGrounded = true;
        col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true; //enable glow
        //Debug.Log("OnCollisionEnter2D");
        }
	}    

    void OnCollisionExit2D(Collision2D col) 
	{
        if (col.gameObject.tag == "Platform")
        {
		isGrounded = false;
        col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled= false; //disable glow
        //Debug.Log("OnCollisionEnter2D");
        }
	}

    void OnTriggerEnter2D(Collider2D col){
        //Debug.Log("OnTriggerEnter2D");
        switch (col.gameObject.tag ){

            case "Death":
                //Death();
                //gameObject.SetActive(false);
                // TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
                //trail.emitting = false;
                alive = false;
                Invoke("Death", 1);
                //StartCoroutine(WaitAndRespawn());
            break;

            case "Life":
                //col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(col.gameObject);
                if (lives<5){
                    lives++;
                }
                break;

            case "Bonus":
                col.gameObject.SetActive(false);
                score++;
                break;

            case "Shield":
                Destroy(col.gameObject);
                break;

            case "Speed":
                Destroy(col.gameObject);
                break;
            
            default:
                break;
        }
    }

    void Death(){
        lives--;
            //check if there enough lives
        if (lives >= 1){
            alive = true;
            float positionY = transform.position.y;
            if (transform.position.y > 0){
                positionY = 4;
            } else positionY = -4;
            transform.position = new Vector3(transform.position.x + 4, positionY, 0);
            Debug.Log("respawn");
            //gameObject.SetActive(true);
           
            trail.emitting = false;
            Invoke("ActivateTrail", 0.13f);
            //gameObject.GetComponent<TrailRenderer>().enabled = true;

        } else {
            //game over screen
            //add end panel
            Invoke("GameOver",2);
        };
    }

    void ActivateTrail(){
        //trail.time = 0.01f;
        trail.emitting = true;
        
        //trail.enabled = true;
        // while (trail.time<0.6f){
        //     trail.time += trail.time;
        // }
        
    }

    public IEnumerator WaitAndRespawn() 
	{	
		yield return new WaitForSeconds (waitingTime);
        //Respawn();
        Death();
	}

    void Respawn(){
        float positionY = transform.position.y;
        if (transform.position.y > 0){
            positionY = 4;
        } else positionY = -4;
        transform.position = new Vector3(transform.position.x + 7, positionY, 0);
    }

    void GameOver(){
            Debug.Log("Game Over");
            Application.Quit();
    }
}
