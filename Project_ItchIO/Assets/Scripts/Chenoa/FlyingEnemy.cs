using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    private Vector3 firstPos;
    private Vector3 secondPos;
    private Vector3 currentPos;

    [SerializeField] private float speed = 1;
    [SerializeField] private float journeyLengthX = 3;
    private Vector3 journeyLength;

    private SpriteRenderer flyingEnemyRenderer;

    private bool goingToSecondPos = true;

    void Start()
    {
        flyingEnemyRenderer = gameObject.GetComponent<SpriteRenderer>();
        flyingEnemyRenderer.flipX = false;

        journeyLength = new Vector3(journeyLengthX, 0, 0);
        firstPos = transform.position;
        secondPos = transform.position += journeyLength;
        transform.position = firstPos;
    }

    void Update()
    {
        currentPos.x = transform.position.x;
        Flying();
    }

    private void Flying()
    {
        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(Time.time * speed, 1));

        if (currentPos.x < secondPos.x && goingToSecondPos == true)
        {
            flyingEnemyRenderer.flipX = false;
            if (currentPos.x > secondPos.x - 0.1f)
            {
                goingToSecondPos = false;
                flyingEnemyRenderer.flipX = true;
            }
        }
        else if (currentPos.x < firstPos.x + 0.1f && goingToSecondPos == false)
        {
            goingToSecondPos = true;
            flyingEnemyRenderer.flipX = true;
        }
    }
}
