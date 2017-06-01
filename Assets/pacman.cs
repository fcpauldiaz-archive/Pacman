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

	// Use this for initialization
	void Start () {
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
		// PS4 MAIN JOYSTICKS
		float h = horizontalSpeed * Input.GetAxis("PS4_RIGHTANALOG_HORIZONTAL");
		float v = rotationSpeed * Input.GetAxis ("PS4_RIGHTANALOG_VERTICAL");
		Debug.Log (v);
		pacm.transform.Rotate(v, h, 0);
		float translation = Input.GetAxis("Vertical") * moveSpeed;
		float translationX = Input.GetAxis("Horizontal") * moveSpeed;
		translation *= Time.deltaTime;
		translationX *= Time.deltaTime;
		pacm.transform.Translate(-translation, 0, 0);
		pacm.transform.Translate(0, 0, translationX);


	}

	void startOnClick()
	{
		menu.SetActive (false);
		cameraMenu.SetActive(false);
		cameraPlayer.SetActive (true);
	}
}
