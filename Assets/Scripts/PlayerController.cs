using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    
    // Use this for initialization
    void Awake()
    {
        // register ourselves with the camera controller
        FindObjectOfType<CameraController>().RegisterPlayer(GetComponent<Rigidbody2D>());
        gameController = FindObjectOfType<GameController>();
        gameController.RegisterPlayer(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            gameController.Win();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Pollen")
        {
            Debug.Log("Hit by pollen!");
            gameController.Hit();
        }
    }
}
