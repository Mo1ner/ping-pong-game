using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float _speed = 7f;
    public Vector2 _startPos = new Vector2(-7.75f, 0f);
    
    void Start()
    {
        transform.localPosition = (Vector3)_startPos;
    }
    void Update()
    {
        MouseMove();
    }

    void KeyboardMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.localPosition += Vector3.up * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.localPosition += Vector3.down * _speed * Time.deltaTime;
        }
    }

    void MouseMove()
    {
        Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouse = Input.mousePosition;
        transform.position = new Vector3(_startPos.x, Camera.main.ScreenToWorldPoint(mouse).y, 0);
    }
}
