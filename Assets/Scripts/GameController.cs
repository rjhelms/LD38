﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Rigidbody2D Player;
    public float MoveForce = 125f;
    public float JumpForce = 300f;
    public float PlayerRadius = 40f;
    public LayerMask GroundLayerMask;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Collider2D grounded = Physics2D.OverlapCircle(Player.transform.position, PlayerRadius, GroundLayerMask);
        Player.AddForce(new Vector2(Input.GetAxis("Horizontal") * MoveForce, 0));

        if (grounded)
        {

            if (Input.GetButton("Fire1"))
            {
                Debug.Log("Jumping!");
                Player.velocity += new Vector2(0, JumpForce);
            }
            else
            {
                Debug.Log("Can't jump");
            }
        }
    }

    public void RegisterPlayer(Rigidbody2D player)
    {
        Player = player;
    }
}