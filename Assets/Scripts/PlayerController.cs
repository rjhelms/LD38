using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MoveForce = 1f;
    public float JumpForce = 1f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal") * MoveForce, 0));
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Jumping!");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
}
