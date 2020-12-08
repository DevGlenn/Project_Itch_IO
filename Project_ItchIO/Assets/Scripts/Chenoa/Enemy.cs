using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public GameObject player;

    public void TakeHit() 
    {
        Die();
	}
    private void Die()
    {
        hp = -1;
        if (hp <= 0) //als het hp kleiner is of gelijk aan 0 is
        {
            Destroy(gameObject); //destroy het gameobject (voor nu)
            //als we de art hebben dan moet de doodgaan animatie spelen
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>(); //de player = zijn eigen collider en dan get hij het script PlayerMovement
        if (player != null && collision.collider == player.PlayerCollider) //als de player bestaat en de de collision de player collider is
        {
            player.TakeDamage(); //doe de TakeDamage functie van het player script
		}
    }
}
