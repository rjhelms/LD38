using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

    public enum GameState { RUNNING, WIN, LOSE };

    public Rigidbody2D Player;
    public float MoveForce = 125f;
    public float JumpForce = 300f;
    public float PlayerRadius = 40f;
    public LayerMask GroundLayerMask;
    public GameState State;
    public bool HitRecovery = false;
    public float HitRecoverTime = 1f;
    public float HitFlashTime = 0.2f;

    private float nextFlashTime;
    private float endRecoverTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (HitRecovery)
        {
            if (Time.fixedTime > endRecoverTime)
            {
                HitRecovery = false;
                Player.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (Time.fixedTime > nextFlashTime)
            {
                Player.GetComponent<SpriteRenderer>().enabled = !Player.GetComponent<SpriteRenderer>().enabled;
                nextFlashTime = Time.fixedTime + HitFlashTime;
            }
        }
        if (State == GameState.RUNNING)
        {
            Collider2D grounded = Physics2D.OverlapCircle(Player.transform.position, PlayerRadius, GroundLayerMask);
            Player.AddForce(new Vector2(Input.GetAxis("Horizontal") * MoveForce, 0));

            if (grounded)
            {
                if (Input.GetButton("Fire1"))
                {
                    Player.velocity += new Vector2(0, JumpForce);
                }
            }
        } else if (State == GameState.WIN)
        {
            Player.AddForce(new Vector2(MoveForce * 3, 0));
        }
    }

    public void RegisterPlayer(Rigidbody2D player)
    {
        Player = player;
    }

    public void Win()
    {
        State = GameState.WIN;
        Debug.Log("You win!");
    }

    public void Hit()
    {
        if (!HitRecovery)
        {
            ScoreManager.Instance.HitPoints--;
            if (ScoreManager.Instance.HitPoints >= 0)
            {
                Debug.Log(string.Format("Ouch! {0} hit points left", ScoreManager.Instance.HitPoints));
                HitRecovery = true;
                nextFlashTime = Time.fixedTime + HitFlashTime;
                endRecoverTime = Time.fixedTime + HitRecoverTime;
                Player.GetComponent<SpriteRenderer>().enabled = false;
            } else
            {
                Debug.Log("You lose!");
                ScoreManager.Instance.Lives -= 1;
            }
        }
    }
}
