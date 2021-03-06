﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public enum GameState { RUNNING, WIN, LOSE };

    public AudioSource AudioPlayer;
    public Rigidbody2D Player;
    public float MoveForce = 125f;
    public float JumpForce = 300f;
    public float PlayerRadius = 40f;
    public LayerMask GroundLayerMask;
    public GameState State;
    public bool HitRecovery = false;
    public float HitRecoverTime = 1f;
    public float HitFlashTime = 0.2f;
    public bool PlayerGrounded;

    public AudioClip LevelClearSound;
    public AudioClip LevelLoseSound;
    public AudioClip HitSound;
    public AudioClip JumpSound;
    public AudioClip SneezeSound;
    public AudioClip PoopSound;
    public AudioClip PowerupSound;
    public AudioClip OneUpSound;

    public Image HealthImage;
    public int HealthImageScaleFactor = 32;
    public Text ScoreText;
    public Text LivesLevelText;

    public int LevelClearScore = 2000;
    public int PowerupScore = 500;

    private float nextFlashTime;
    private float endRecoverTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // debug level advance
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Win();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Lose();
        }

        if (ScoreManager.Instance.Score > ScoreManager.Instance.NextNewLifeScore)
        {
            ScoreManager.Instance.Lives++;
            ScoreManager.Instance.NextNewLifeScore *= 2;
            AudioPlayer.PlayOneShot(OneUpSound);
        }

        ScoreText.text = ScoreManager.Instance.Score.ToString();

		if (State == GameState.RUNNING)
        {
            HealthImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ScoreManager.Instance.HitPoints * HealthImageScaleFactor);
            LivesLevelText.text = string.Format("LIVES: {0}\r\nLEVEL: {1}", ScoreManager.Instance.Lives, ScoreManager.Instance.Level);
        }
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

        if (!HitRecovery)
        {
            Player.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (State == GameState.RUNNING)
        {
            if (Player.transform.position.y < -32)
            {
                Lose();
            }

            Player.AddForce(new Vector2(Input.GetAxis("Horizontal") * MoveForce, 0));

            if (PlayerGrounded)
            {
                if (Input.GetButton("Fire1"))
                {
                    Collider2D grounded = Physics2D.OverlapCircle(Player.transform.position, PlayerRadius, GroundLayerMask);
                    if (grounded)
                    {
                        PlayerGrounded = false;
                        Player.velocity += new Vector2(0, JumpForce);
                        AudioPlayer.PlayOneShot(JumpSound);
                    }
                }
            }
        } else if (State == GameState.WIN)
        {
            Player.AddForce(new Vector2(MoveForce * 3, 0));
            if (!AudioPlayer.isPlaying)
            {
                ScoreManager.Instance.Level++;
                if (ScoreManager.Instance.Level <= 5)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("main");
                } else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("win");
                }
            }
        } else if (State == GameState.LOSE)
        {
            if (!AudioPlayer.isPlaying)
            {
                ScoreManager.Instance.Lives--;
                if (ScoreManager.Instance.Lives >= 0)
                {
                    ScoreManager.Instance.HitPoints = ScoreManager.Instance.DefaultHitPoints;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("main");
                } else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("lose");
                }
            }
        }
    }

    public void RegisterPlayer(Rigidbody2D player)
    {
        Player = player;
    }

    public void Win()
    {
        if (State == GameState.RUNNING)
        {
            State = GameState.WIN;
            Debug.Log("You win!");
            AudioPlayer.PlayOneShot(LevelClearSound);
            ScoreManager.Instance.Score += LevelClearScore;
        }
    }

    public void Lose()
    {
        if (State == GameState.RUNNING)
        {
            Debug.Log("You lose!");
            AudioPlayer.PlayOneShot(LevelLoseSound);
            State = GameState.LOSE;
            Player.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Hit()
    {
        if ((State == GameState.RUNNING) & !HitRecovery)
        {
            ScoreManager.Instance.HitPoints--;
            if (ScoreManager.Instance.HitPoints >= 0)
            {
                Debug.Log(string.Format("Ouch! {0} hit points left", ScoreManager.Instance.HitPoints));
                HitRecovery = true;
                nextFlashTime = Time.fixedTime + HitFlashTime;
                endRecoverTime = Time.fixedTime + HitRecoverTime;
                Player.GetComponent<SpriteRenderer>().enabled = false;
                AudioPlayer.PlayOneShot(HitSound);
            } else
            {
                Lose();
            }
        }
    }

    public void PlaySneeze()
    {
        AudioPlayer.PlayOneShot(SneezeSound);
    }

    public void PlayPoop()
    {
        AudioPlayer.PlayOneShot(PoopSound);
    }

    public void Powerup()
    {
        if (State == GameState.RUNNING)
        {
            AudioPlayer.PlayOneShot(PowerupSound);
            ScoreManager.Instance.Score += PowerupScore;
            if (ScoreManager.Instance.HitPoints < ScoreManager.Instance.MaxHitPoints)
            {
                ScoreManager.Instance.HitPoints++;

            }
        }
    }
}
