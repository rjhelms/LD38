using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public Vector2 FlyForceVector;
    public Transform PoopEmmiter;
    public GameObject PoopPrefab;

    public float PoopCheckTime;
    public float PoopChance;
    public float TurnAroundTime;

    private GameController gameController;
    private Rigidbody2D rigidBody;
    private float nextPoop;
    private float nextTurnAround;
    private Transform projectileParent;
    // Use this for initialization
    void Start () {
        gameController = FindObjectOfType<GameController>();
        rigidBody = GetComponent<Rigidbody2D>();
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
        nextPoop = Time.fixedTime + PoopCheckTime;
        nextTurnAround = Time.fixedTime + TurnAroundTime;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (gameController.State == GameController.GameState.RUNNING)
        {
            rigidBody.AddRelativeForce(Vector2.Scale(FlyForceVector, transform.localScale));
            if (Time.fixedTime > nextTurnAround)
            {
                transform.localScale = Vector2.Scale(transform.localScale, new Vector2(-1, 1));
                rigidBody.velocity = Vector2.zero;
                nextTurnAround = Time.fixedTime + TurnAroundTime;
            }
            if (Time.fixedTime > nextPoop)
            {
                nextPoop = Time.fixedTime + PoopCheckTime;
                float willPoop = Random.Range(0f, 1f);
                if (willPoop <= PoopChance)
                {
                    Instantiate(PoopPrefab, PoopEmmiter.position, Quaternion.identity, projectileParent);
                }
            }
        }
    }
}
