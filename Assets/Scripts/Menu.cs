using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	//function click for the button
    public void button_click()
    {
        SceneManager.LoadScene(1);
    }
}
