using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    private Vector3 firstPos;
    private Vector3 secondPos;
    private Vector3 currentPos;

    [SerializeField] private float speed = 1;
    [SerializeField] private float journeyLengthX = 3;
    private Vector3 journeyLength;

    private bool isGrounded = false;

    private SpriteRenderer walkingEnemyRenderer;

    private bool goingToSecondPos = true;

    void Start()
    {
        walkingEnemyRenderer = gameObject.GetComponent<SpriteRenderer>();
        walkingEnemyRenderer.flipX = false;

        journeyLength = new Vector3(journeyLengthX, 0, 0);
        firstPos = transform.position;
        secondPos = transform.position += journeyLength;
        transform.position = firstPos;
    }

    void Update()
    {
        if (isGrounded == true)
        {
            currentPos.x = transform.position.x;
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
        
        if (currentPos.x < secondPos.x && goingToSecondPos == true)
        {
            walkingEnemyRenderer.flipX = false;
            if (currentPos.x > secondPos.x - 0.1f)
            {
                goingToSecondPos = false;
                walkingEnemyRenderer.flipX = true;
            }
        }
        else if (currentPos.x < firstPos.x + 0.1f && goingToSecondPos == false)
        {
            goingToSecondPos = true;
            walkingEnemyRenderer.flipX = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
