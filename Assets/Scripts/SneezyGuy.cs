using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezyGuy : MonoBehaviour {

    public ParticleSystem SneezeEmitter;
    public float SneezeCheckTime;
    public float SneezeChance;
    public float SneezePauseTime;
    public Vector2 WalkVector;
    public LayerMask GroundLayerMask;
    public LayerMask ObstacleLayerMask;
    public Vector2 LookDownVector = new Vector2(64, -64);
    public Vector2 LookAheadVector = new Vector2(64, 0);
    public float LookAheadDistance = 64f;
    public Sprite[] AnimationSprites;
    public int currentSprite = 0;
    public float AnimationSpeed;
    public float AnimationVelocityFactor;

    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private float nextSneezeCheckTime;
    private float walkSuspendTime = 0;
    private Collider2D myCollider;
    private float currentFrameTime;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        nextSneezeCheckTime = Time.fixedTime + SneezeCheckTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (gameController.State == GameController.GameState.RUNNING)
        {
            if (Time.fixedTime > walkSuspendTime)
            {
                bool turnAround = false;

                // check if there is ground ahead/below
                bool ground_ahead = false;
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.Scale(LookDownVector, transform.localScale), LookAheadDistance, GroundLayerMask);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != myCollider)
                    {
                        ground_ahead = true;
                    }
                }
                if (!ground_ahead)
                {
                    turnAround = true;
                }

                // check if there is an obstacle in front
                hits = Physics2D.RaycastAll(transform.position, Vector2.Scale(LookAheadVector, transform.localScale), LookAheadDistance, ObstacleLayerMask);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != myCollider)
                    {
                        turnAround = true;
                    }
                }

                if (!turnAround)
                {
                    rigidBody.AddForce(Vector2.Scale(WalkVector, transform.localScale));
                } else
                {
                    transform.localScale = Vector2.Scale(transform.localScale, new Vector2(-1, 1));
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }

                if (Time.fixedTime > nextSneezeCheckTime)
                {
                    nextSneezeCheckTime = Time.fixedTime + SneezeCheckTime;
                    float willSneeze = Random.Range(0f, 1f);
                    if (willSneeze <= SneezeChance)
                    {
                        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                        walkSuspendTime = Time.fixedTime + SneezePauseTime;
                        SneezeEmitter.Play();
                        if (spriteRenderer.isVisible)
                        {
                            gameController.PlaySneeze();
                        }
                    }
                }

                currentFrameTime += Time.fixedDeltaTime * Mathf.Abs(rigidBody.velocity.x) * AnimationVelocityFactor;
                if (currentFrameTime > AnimationSpeed)
                {
                    currentFrameTime = 0;
                    currentSprite++;
                    if (currentSprite == AnimationSprites.Length)
                    {
                        currentSprite = 0;
                    }
                    spriteRenderer.sprite = AnimationSprites[currentSprite];
                }
            }
        }
    }
}
