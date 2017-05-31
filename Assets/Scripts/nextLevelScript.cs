using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevelScript : MonoBehaviour {

	public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //load the next level
    }
}
