using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public Vector2 _direction;
    private bool _assignedPoint;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 7f;

    Game _game;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _direction = new Vector2(Random.Range(.5f, 1f), Random.Range(.5f, 1f));
        _game = GameObject.Find("Game").GetComponent<Game>();
    }

    void Update()
    {
        if (_game._gameState != Game.GameState.Paused)
        {
            BallMove();
        }
    }

    void BallMove()
        {
            _rb.velocity = _direction.normalized * _speed;

            if (transform.position.x < -9)
            {
                if (!_assignedPoint)
                {
                    _assignedPoint = true;
                    _game.ComputerPoint();
                }
            }

            if (transform.position.x > 9)
            {
                if (!_assignedPoint)
                {
                    _assignedPoint = true;
                    _game.PlayerPoint();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Top Wall"))
            {
                _direction.y = -_direction.y;
            }

            if (col.gameObject.CompareTag("Bottom Wall"))
            {
                _direction.y = -_direction.y;
            }

            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Computer"))
            {
                _direction.x = -_direction.x;
            }
        }
    }
