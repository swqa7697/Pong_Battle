using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player0: MonoBehaviour
{
    public float mySpeed, yMin, yMax;

    private Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            input += Vector2.down;

        input.Normalize();

        Vector2 newPosition = myRigidbody.position + input * mySpeed * Time.deltaTime;
        newPosition = new Vector2(newPosition.x, Mathf.Clamp(newPosition.y, yMin, yMax));

        myRigidbody.MovePosition(newPosition);
    }
}
