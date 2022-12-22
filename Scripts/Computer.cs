using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField]private float _speed = 8f;
    private GameObject _ball;
    private Vector2 _startPos = new Vector2(7.75f, 0f);
    private Vector2 _ballPos;
    void Start()
    {
        transform.localPosition = (Vector3)_startPos;
    }
    
    void Update()
    {
        MoveComputer();
    }

    void MoveComputer()
    {
        if (_ball != true)
            _ball = GameObject.FindGameObjectWithTag("Ball");

        if (_ball == true)
        {
            if (_ball.GetComponent<Ball>()._direction.x >= 0f)
            {
                _ballPos = _ball.transform.localPosition;
            }
        }

        if ( _ballPos.y < transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0f, -_speed * Time.deltaTime, 0f);
        }

        if (_ballPos.y > transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0f, _speed * Time.deltaTime, 0f);
        }
    }
}
