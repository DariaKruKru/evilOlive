using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public int lives;
	public int numOfHearts;
	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;
	public Image energy;
	public Text score;
	public Text distance;
	public bool isPaused = false;
	public GameObject pausePanel;
	public GameObject character;
	[SerializeField] StaticBluredScreen m_StaticBluredScreen;

	
	// Use this for initialization
	void Start () {
		character = GameObject.FindWithTag("Player");
		lives = character.GetComponent<CharacterController2D>().lives;
		pausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		//exit
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("space key was pressed");
            Application.Quit();
        }

		//pause
		if(Input.GetKeyDown (KeyCode.P)) 
        {
			PauseButton();
        } 

		if(Input.GetKeyDown (KeyCode.R)) 
        {
			Restart();
        } 

		//lives counter
		//lives = CharacterController2D.lives;
		lives = character.GetComponent<CharacterController2D>().lives;
		for(int i=0; i < hearts.Length; i++)
		{
			if (i < lives){
				hearts[i].sprite = fullHeart;
			} else {
				hearts[i].sprite = emptyHeart;
			} 

			if (i<numOfHearts){
				hearts[i].enabled=true;
			} else {
				hearts[i].enabled = false;
			}
		}

		//energy bar

		//score board
		score.text = character.GetComponent<CharacterController2D>().score.ToString();
		distance.text = character.GetComponent<CharacterController2D>().distanceTraveled.ToString();
	}

	public void Restart(){
		string thisScene = SceneManager.GetActiveScene ().name;
		//SceneManager.UnloadSceneAsync(thisScene);
		// GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
 
		// for (int i = 0; i < GameObjects.Length; i++)
		// {
		// 	Destroy(GameObjects[i]);
		// }
		
		SceneManager.LoadScene("SampleScene_vertical");
		Time.timeScale = 1;
		//SceneManager.LoadScene("RestartUtilite");
	}

	public void MainMenu() {
		SceneManager.LoadScene("MainMenu");

	}


	private void PauseGame()
    {
		m_StaticBluredScreen.Capture();
        Time.timeScale = 0;
		isPaused = true;
        pausePanel.SetActive(true);
		
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1;
		isPaused = false;
        pausePanel.SetActive(false);
		m_StaticBluredScreen.texture = null;
        //enable the scripts again
    }

	public void PauseButton(){
		if (!isPaused) 
		{
			PauseGame();
		} else if (isPaused) 
		{
			ContinueGame();   
		}
	}
}
