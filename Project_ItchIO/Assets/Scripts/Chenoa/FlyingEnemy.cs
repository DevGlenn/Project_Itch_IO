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

    private bool goingToSecondPos = true;

    public Animator animator; //Enemy.cs moet erbij dus daarom is het public

    void Start()
    {
        base.Start();        
        journeyLength = new Vector3(journeyLengthX, 0, 0);
        firstPos = transform.position;
        secondPos = transform.position += journeyLength;
        transform.position = firstPos;

        animator = gameObject.GetComponent<Animator>();
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }

    void Update()
    {
        currentPos.x = transform.position.x;
        if (flyingEnemyIsDead == false)
        {
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
            Flying();
        }
    }

    private void Flying()
    {
        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(Time.time * speed, 1.0f)); //lerp van first pos naar second pos en terug

        if (currentPos.x < secondPos.x && goingToSecondPos == true) //als dit gameobject op de x as nog niet bij de second pos op x as is en je bent er wel naartoe aan het gaan
        {
            if (currentPos.x >= secondPos.x - 0.1f) //als je bijna bij de secondpos bent 
            {
                Debug.Log("rechterkant!");
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

                goingToSecondPos = false; //ga dan weer terug naar de eerste pos
            }
        }
        else if (currentPos.x >= firstPos.x && goingToSecondPos == false) //anders als dit gameobject op de x as nog niet bij de first pos op x as is en je bent niet naar de second pos toe aan het gaan (dus naar de eerste pos)
        {
            if (currentPos.x <= firstPos.x + 0.1f)
            {
                Debug.Log("linkerkant!");
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

                goingToSecondPos = true; //zet dan dat je naar de second pos gaat op true
            }
        }
    }
}
