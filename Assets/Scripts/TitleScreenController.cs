using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    public GameObject MusicPlayer;
	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Music") == null)
        {
            GameObject music = Instantiate(MusicPlayer);
            DontDestroyOnLoad(music);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        }
	}
}
