﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public GameObject player;

    private FlyingEnemy flyingEnemyScript;
    private Rigidbody2D flyingEnemyRigidbody2D;
    private WalkingEnemy walkingEnemyScript;
    private Rigidbody2D walkingEnemyRigidbody2D;

    public bool flyingEnemyIsDead;
    public bool walkingEnemyIsDead;

    public void Start()
    {
        flyingEnemyScript = FindObjectOfType<FlyingEnemy>();
        walkingEnemyScript = FindObjectOfType<WalkingEnemy>();
        flyingEnemyRigidbody2D = flyingEnemyScript.gameObject.GetComponent<Rigidbody2D>();
        walkingEnemyRigidbody2D = walkingEnemyScript.gameObject.GetComponent<Rigidbody2D>();
        flyingEnemyIsDead = false;
        walkingEnemyIsDead = false;
    }

    public void TakeHit() 
    {
        Die();
	}

    private void Die()
    {
        hp = -1;
        if (hp <= 0 && gameObject.tag == "FlyingEnemy") //als het hp kleiner is of gelijk aan 0 is
        {
            flyingEnemyIsDead = true;
            flyingEnemyScript.animator.SetBool("IsDead", true); //speel de animatie af
            flyingEnemyRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //freezed de positie op de Y as niet meer zodat hij naar beneden valt
            flyingEnemyScript.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true; //maakt de draak een trigger want dan valt hij door alles heen
        }
        if (hp <= 0 && gameObject.tag == "WalkingEnemy")
        {
            walkingEnemyIsDead = true;
            walkingEnemyScript.animator.SetBool("IsDead", true); //speel de animatie af
            walkingEnemyRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //freezed de positie op de Y as niet meer zodat hij naar beneden valt
            walkingEnemyScript.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true; //maakt de draak een trigger want dan valt hij door alles heen
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>(); //de player = zijn eigen collider en dan get hij het script PlayerMovement
        if (player != null && collision.collider == player.PlayerCollider) //als de player bestaat en de de collision de player collider is
        {
            Debug.Log(collision.collider);
            player.TakeDamage(); //doe de TakeDamage functie van het player script
		}
    }
}
