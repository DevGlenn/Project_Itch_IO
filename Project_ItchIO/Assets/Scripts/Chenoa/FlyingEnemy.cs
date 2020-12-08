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
        flyingEnemyRenderer.flipX = true;

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
        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(Time.time * speed, 1)); //lerp van first pos naar second pos en terug

        if (currentPos.x < secondPos.x && goingToSecondPos == true) //als dit gameobject op de x as nog niet bij de second pos op x as is en je bent er wel naartoe aan het gaan
        {
            flyingEnemyRenderer.flipX = true; //de flipX = true want dan kijkt hij naar rechts
            if (currentPos.x > secondPos.x - 0.1f) //als je bijna bij de secondpos bent 
            {
                goingToSecondPos = false; //ga dan weer terug naar de eerste pos
                flyingEnemyRenderer.flipX = false; //en flip de sprite zodat je naar links kijkt
            }
        }
        else if (currentPos.x < firstPos.x + 0.1f && goingToSecondPos == false) //anders als dit gameobject op de x as nog niet bij de first pos op x as is en je bent niet naar de second pos toe aan het gaan (dus naar de eerste pos)
        {
            goingToSecondPos = true; //zet dan dat je naar de second pos gaat op true
            flyingEnemyRenderer.flipX = false; //en kijk de andere kant op
        }
    }
}
