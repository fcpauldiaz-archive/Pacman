using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour {

	public GameObject menu;
	public Button startButton;

	// Use this for initialization
	void Start () {
		startButton = GameObject.Find("Continue").GetComponent<Button>();
		startButton.onClick.AddListener(TaskOnClick);
		menu = GameObject.Find ("MainMenu");


	}

	void TaskOnClick()
	{
		menu.SetActive (false);
	}

}
