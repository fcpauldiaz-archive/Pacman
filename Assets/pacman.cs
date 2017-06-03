using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pacman : MonoBehaviour {

	public GameObject pacm;
	public bool firstPersonView = false;
	public float moveSpeed = 10000f;
	public float rotationSpeed = 50f;
	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;
	public GameObject cameraPlayer;
	public GameObject cameraMenu;
	public Button newGame;
	public GameObject menu;
	public Button startButton;
	public bool gamePaused = true;
	public bool inversePlay = false;
	public int score;

	// Use this for initialization
	void Start () {
		gamePaused = true;
		pacm = GameObject.Find ("pacman");
		cameraPlayer = GameObject.Find ("Camera");
		cameraMenu = GameObject.Find ("CameraMenu");
		menu = GameObject.Find ("MainMenu");

		cameraPlayer.SetActive (false);
		startButton = GameObject.Find("Continue").GetComponent<Button>();
		startButton.onClick.AddListener(startOnClick);
		menu = GameObject.Find ("MainMenu");

		Debug.Log ("test");

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
		float h = horizontalSpeed * Input.GetAxis("PS4_RIGHTANALOG_HORIZONTAL");
		float v = rotationSpeed * Input.GetAxis ("PS4_RIGHTANALOG_VERTICAL");
		pacm.transform.Rotate(0, h, 0);
		// PS4 MAIN JOYSTICKS
		float translation = Input.GetAxis("Vertical") * moveSpeed;
		float translationX = Input.GetAxis("Horizontal") * moveSpeed;
		translation *= Time.deltaTime;
		translationX *= Time.deltaTime;
		pacm.transform.Translate(-translation, 0, 0);
		pacm.transform.Translate(0, 0, translationX);

		cameraPlayer.transform.Rotate (v, 0, 0);

	}

	public void startOnClick()
	{
		menu.SetActive (false);
		cameraMenu.SetActive(false);
		cameraPlayer.SetActive (true);
		gamePaused = false;

	}

	public void OnCollisionEnter (Collision col) {
		
		Debug.Log (col.gameObject.name);

		// Physics.IgnoreCollision(col.gameObject.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());

	}

	public void OnTriggerEnter(Collider col) {
		Debug.Log ("Trigger");

		if (col.gameObject.name.Contains("apple")) {
			score = score + 1;
			Destroy(col.gameObject);
		}
		if (col.gameObject.name.Contains("cherry")) {
			Destroy(col.gameObject);
		}
	}

	public void OnGUI(){
		if (gamePaused == false) {
			Debug.Log (Screen.width);
			GUI.Box(new Rect(Screen.width/2+200,Screen.height/2-150,100,25), "Score " + score);
		}
	}


}
