using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezyGuy : MonoBehaviour {

    public ParticleSystem SneezeEmitter;
    public float SneezeCheckTime;
    public float SneezeChance;

    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private float nextSneezeCheckTime;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextSneezeCheckTime = Time.fixedTime + SneezeCheckTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (gameController.State == GameController.GameState.RUNNING)
        {
            if (Time.fixedTime > nextSneezeCheckTime)
            {
                nextSneezeCheckTime = Time.fixedTime + SneezeCheckTime;
                float willSneeze = Random.Range(0f, 1f);
                if (willSneeze <= SneezeChance)
                {
                    Debug.Log(willSneeze);
                    Debug.Log("Achoo!");
                    SneezeEmitter.Play();
                    if (spriteRenderer.isVisible)
                    {
                        gameController.PlaySneeze();
                    }
                }
            }
        }
    }
}
