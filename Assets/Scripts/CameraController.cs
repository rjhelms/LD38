using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Rigidbody2D Player;
    public float ForecastMultiplier;
    public float LerpFactor = 0.2f;
    public float TargetY = 96;
    public float TargetZ = -10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float x_position = Player.transform.position.x;
        x_position += Player.velocity.x * ForecastMultiplier;
        x_position = Mathf.Lerp(transform.position.x, x_position, LerpFactor);
        transform.position = new Vector3(x_position, TargetY, TargetZ);
	}
}
