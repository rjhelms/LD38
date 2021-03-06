﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Rigidbody2D Player;
    public float ForecastMultiplier;
    public float LerpFactor = 0.2f;
    public float TargetY = 96;
    public float TargetZ = -10;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameController.State == GameController.GameState.RUNNING)
        {
            float x_position = Player.transform.position.x;
            x_position += Player.velocity.x * ForecastMultiplier;
            x_position = Mathf.Lerp(transform.position.x, x_position, LerpFactor);
            transform.position = new Vector3(x_position, TargetY, TargetZ);
        }
	}

    public void RegisterPlayer(Rigidbody2D player)
    {
        Player = player;
        transform.position = new Vector3(Player.transform.position.x, TargetY, TargetZ);
    }
}
