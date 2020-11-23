using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    private Vector3 firstPos;
    private Vector3 secondPos;

    [SerializeField] private float speed;
    [SerializeField] private Vector3 journeyLength = new Vector3(5f, 0f, 0f);

    private bool isGrounded = false;

    void Start()
    {
        firstPos = transform.position;
        secondPos = transform.position += journeyLength;
        transform.position = firstPos;
    }

    void Update()
    {
        if (isGrounded == true)
        {
            Walking();
        }
        else
        {
            return;
        }
    }

    private void Walking()
    {
        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }


}
