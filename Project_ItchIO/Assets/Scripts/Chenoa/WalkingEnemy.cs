using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    private Vector3 firstPos; //start positie
    private Vector3 secondPos; //eind positie
    private Vector3 currentPos; //de positie waar het object zich nu bevindt

    [SerializeField] private float speed = 1; //hoe snel de enemy gaat
    [SerializeField] private float journeyLengthX = 3; //hoe ver de afstand is dat hij aflegt 
    private Vector3 journeyLength; //dit is de positie van de journeyLength

    private bool isGrounded = false; 

    private bool goingToSecondPos = true;

    //Vector3 lTemp;

    void Start()
    {
        base.Start();

        journeyLength = new Vector3(journeyLengthX, 0, 0); //maak een nieuwe vector3 aan van de journeyLength positie
        firstPos = transform.position; //first pos is de positie waar die op begint
        secondPos = transform.position += journeyLength; //de secondpos is de positie waar hij op begint + de journeyLength
        transform.position = firstPos; //hij begint op de firstpos

        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }

    void Update()
    {
        currentPos.x = transform.position.x; //de positie waar die nu is, is zijn transform.position
        if (walkingEnemyIsDead == false) //als de walking enemy op de grond is
        {
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
            Walking(); //voer de Walking functie uit
        }
    }

    private void Walking()
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
