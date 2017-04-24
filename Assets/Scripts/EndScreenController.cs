using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            ScoreManager.Instance.Reset();
            UnityEngine.SceneManagement.SceneManager.LoadScene("titlescreen");
        }
	}
}
