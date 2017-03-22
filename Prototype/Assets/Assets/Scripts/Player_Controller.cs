﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public int Player = 0;

    [Header("Controls")]
    public KeyCode CatchKey;
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;

    [Header("Movement")]
    public float Speed = 7f;
    private float MoveX = 0f;
    private float MoveY = 0f;

    public enum ThrowDirection
    {
        Straight,
        Up,
        Down
    }

    private int ThrowCharge = 0;
    private ThrowDirection ThrowDir = ThrowDirection.Straight;
    private bool BallCaught = false;
    private GameObject HeldBall;
    
    [Header("Player Selection")]
    private GameObject Game_Controller;
    private ValueStorer GameC_Script;

    public Sprite Penguin;
    public Sprite PolarBear;
    public Sprite Reindeer;
    public Sprite Wolf;

    public SpriteRenderer ThisSprite;

    // Use this for initialization
    void Start()
    {
        Game_Controller = GameObject.Find("GameController");
        GameC_Script = Game_Controller.GetComponent<ValueStorer>();

        ThisSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (HeldBall == null)
        {
            foreach (GameObject Ball in Ball_Controller.Balls)
            {
                BallDistance(Ball);

                // can only catch one ball at a time.
                if (HeldBall != null)
                    break;
            }
        }

        if (Input.GetKey(LeftKey))
            MoveX = 1;
        else if (Input.GetKey(RightKey))
            MoveX = -1;
        else
            MoveX = 0;

        if (Input.GetKey(UpKey))
            MoveY = 1;
        else if (Input.GetKey(DownKey))
            MoveY = -1;
        else
            MoveY = 0;

        if (BallCaught)
        {
            if (Input.GetKeyDown(UpKey))
            {
                ThrowDir = ThrowDirection.Up;
            }

            if (Input.GetKeyDown(DownKey))
            {
                ThrowDir = ThrowDirection.Down;
            } 
        }

		if ((Input.GetKeyUp(UpKey)) || (Input.GetKeyUp(DownKey)))
		{
            ThrowDir = ThrowDirection.Straight;
		}

        GetComponent<Rigidbody2D>().velocity = new Vector2(MoveX * Speed, MoveY * Speed);

        if (BallCaught)
        {
            if (Input.GetKeyUp(CatchKey))
            {
                HeldBall.SetActive(true);
                HeldBall.GetComponent<Ball_Controller>().Throw(Player, ThrowCharge, transform.position, ThrowDir);
                HeldBall = null;
                BallCaught = false;
                ThrowCharge = 0;
            }

            if (Input.GetKeyDown(CatchKey))
            {
                StartCoroutine(Timer());
            }
        }

        if (Player == 1)
        {
            switch (GameC_Script.Player1Character)
            {
                case 0:
                    ThisSprite.sprite = Penguin;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(0.4f, 0.4f, 0);
                    break;
                case 1:
                    ThisSprite.sprite = PolarBear;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(0.4f, 0.4f, 0);
                    break;
                case 2:
                    ThisSprite.sprite = Reindeer;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(1.5f, 1.5f, 0);
                    break;
                case 3:
                    ThisSprite.sprite = Wolf;
                    break;
            }
        }

        if (Player == 2)
        {
            switch (GameC_Script.Player2Character)
            {
                case 0:
                    ThisSprite.sprite = Penguin;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(0.4f, 0.4f, 0);
                    break;
                case 1:
                    ThisSprite.sprite = PolarBear;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(0.4f, 0.4f, 0);
                    break;
                case 2:
                    ThisSprite.sprite = Reindeer;
                    Destroy(GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                    transform.localScale = new Vector3(1.5f, 1.5f, 0);
                    break;
                case 3:
                    ThisSprite.sprite = Wolf;
                    break;
            }
        }
    }

    void BallDistance(GameObject Ball)
    {
        if (Ball != null)
        {
            if (Vector3.Distance(transform.position, Ball.transform.position) < 2.0f && Input.GetKeyDown(CatchKey))
            {
                BallCaught = true;
                StartCoroutine (Timer());
                HeldBall = Ball;
                HeldBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                HeldBall.SetActive(false);
            }
        }
    }

    IEnumerator Timer()
    {
        ThrowCharge++;

        if (ThrowCharge > 4)
            ThrowCharge = 4;

        yield return new WaitForSeconds(0.25f);
    }
}