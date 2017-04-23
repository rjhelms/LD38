using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody2D myRigidbody;
    
    // Use this for initialization
    void Awake()
    {
        // register ourselves with the camera controller
        myRigidbody = GetComponent<Rigidbody2D>();
        FindObjectOfType<CameraController>().RegisterPlayer(myRigidbody);
        gameController = FindObjectOfType<GameController>();
        gameController.RegisterPlayer(myRigidbody);
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
        } else if (collision.tag == "Powerup")
        {
            gameController.Powerup();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            bool falling = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y)
                {
                    falling = true;
                }
            }
            if (falling)
            {
                gameController.PlayerGrounded = true;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "EnemyParticle")
        {
            Debug.Log("Hit by EnemyParticle!");
            gameController.Hit();
        }
    }
}
