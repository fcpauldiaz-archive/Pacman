using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pacman : MonoBehaviour {

	public GameObject pacm;
	public bool firstPersonView = false;
	public float moveSpeed = 10000f;
	public float rotationSpeed = 2.0f;
	public float horizontalSpeed = 50F;
	public float verticalSpeed = 2.0F;
	public GameObject cameraPlayer;
	public GameObject cameraMenu;
	public Button newGame;
	public GameObject menu;
	public Button startButton;
	public Button newGameButton;
	public GameObject[] ghosts;
	public bool gamePaused = true;
	public bool inversePlay = false;
	public static bool resetGame = false;
	public int score;
	public float targetTime = 5.0f;
	public AudioSource[] allAudio;

	// Use this for initialization
	void Start () {
		gamePaused = true;
		pacm = GameObject.Find ("pacman");
		cameraPlayer = GameObject.Find ("Camera");
		cameraMenu = GameObject.Find ("CameraMenu");
		menu = GameObject.Find ("MainMenu");
		ghosts = GameObject.FindGameObjectsWithTag ("ghost");
		cameraPlayer.SetActive (false);
		startButton = GameObject.Find("Continue").GetComponent<Button>();
		newGameButton = GameObject.Find ("Newgame").GetComponent<Button> ();
		allAudio  = GetComponents<AudioSource>();
		startButton.onClick.AddListener(startOnClick);
		newGameButton.onClick.AddListener (newGameClick);
		menu = GameObject.Find ("MainMenu");
		if (resetGame == true) {
			startOnClick ();
		}
		resetGame = false;


	}
		
	
	// Update is called once per frame
	void Update () {

		// PS4 Rectangle button
		if (Input.GetKeyDown ("joystick button 0")){
			firstPersonView = !firstPersonView;
			if (firstPersonView == true) {
				Camera.main.transform.Translate(Vector3.forward * 25f);  
			} else {
				Camera.main.transform.Translate(Vector3.back * 25f);  
			}


		}
		if (gamePaused == false) {
			foreach (GameObject gh in ghosts)
			{
				// pacman runs away from ghosts
				if (inversePlay == false) {
					float step = (50f + Random.Range(-10.0f, 30.0f)) * Time.deltaTime;
					gh.transform.position = Vector3.MoveTowards(gh.transform.position, pacm.transform.position, step);
				}
				// ghosts run away from pacman
				else {
					gh.transform.LookAt (pacm.transform.position);
					gh.transform.Rotate(0, 180 + Random.Range(-30.0f, 30.0f) , 0); 
					gh.transform.Translate(Vector3.forward); 
				}


			}

			//PS4 OPTIONS BUTTON
			if (Input.GetKeyDown ("joystick button 9")) {
				gamePaused = !gamePaused;
				if (gamePaused == true) {
					cameraMenu.SetActive (true);
					menu.SetActive (true);
					cameraPlayer.SetActive (false);
				} else {
					cameraMenu.SetActive (false);
					menu.SetActive (false);
					cameraPlayer.SetActive (true);
				}


			}
			// PS4 MAIN JOYSTICKS
			float h = horizontalSpeed * Input.GetAxis("PS4_RIGHTANALOG_HORIZONTAL");
			float v = rotationSpeed * Input.GetAxis ("PS4_RIGHTANALOG_VERTICAL");
			pacm.transform.Rotate(0, h, 0);

			float translation = Input.GetAxis("Vertical") * moveSpeed;
			float translationX = Input.GetAxis("Horizontal") * moveSpeed;
			translation *= Time.deltaTime;
			translationX *= Time.deltaTime;
			pacm.transform.Translate(-translation, 0, 0);
			pacm.transform.Translate(0, 0, translationX);
			//camera rotate but not the player.
			cameraPlayer.transform.Rotate (v, 0, 0);

			if (inversePlay == true) {
				targetTime -= Time.deltaTime;
				if (targetTime <= 0.0f) {
					timerEnded();
				}

			}

			if (score >= 30) {
				//player wins or finish this level.

			}
		}


	}

	//remove menu and start game
	public void startOnClick() {
		menu.SetActive (false);
		cameraMenu.SetActive(false);
		cameraPlayer.SetActive (true);
		gamePaused = false;
		allAudio [0].Stop ();
		allAudio [1].Play ();

	}

	public void OnCollisionEnter (Collision col) {
		
		// Physics.IgnoreCollision(col.gameObject.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());

	}

	public void OnTriggerEnter(Collider col) {
		Debug.Log (col.gameObject.name);

		if (col.gameObject.name.Contains("apple")) {
			score = score + 1;
			Destroy(col.gameObject);
		}
		if (col.gameObject.name.Contains("cherry")) {
			
			inversePlay = true;
			allAudio [1].Stop ();
			allAudio [4].Play ();
			Destroy(col.gameObject);
		}
		if (col.gameObject.name.Contains ("ghost")) {
			//lost game or one heart
			if (inversePlay == false) {
				allAudio [1].Stop ();
				allAudio [2].Play ();
				gamePaused = true;
				cameraMenu.SetActive (true);
				menu.SetActive (true);
				cameraPlayer.SetActive (false);
				SceneManager.LoadScene("Sample");
			} else {
				allAudio [3].Play ();
				//earn points for eating a ghost
				score = score + 10;
				//reset ghost position to some standard
				Vector3 temp = col.gameObject.transform.position; // copy to an auxiliary variable...
				temp.x = 82f;
				temp.y = -15f;
				temp.z = -42.6f;
				col.gameObject.transform.position = temp;
			}

		}
	}

	public void OnGUI(){
		if (gamePaused == false) {
			GUI.Box(new Rect(Screen.width/2+200,Screen.height/2-150,100,25), "Score " + score);
		}
	}

	public void timerEnded() {
		inversePlay = false;
		allAudio [4].Stop ();
		allAudio [1].Play ();

	}

	public void newGameClick() {
		resetGame = true;
		SceneManager.LoadScene("Sample");


	}
		


}
