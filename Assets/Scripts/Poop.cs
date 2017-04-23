using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour {

    public GameObject Parent;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Parent)
        {
            Debug.Log(string.Format("Hit {0}", collision.gameObject));
            if (collision.gameObject.tag == "Player")
            {
                gameController.Hit();
            }
            Destroy(gameObject);
        }
    }
}
