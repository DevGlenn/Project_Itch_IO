using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public GameObject player;

    public void Die()
    {
        hp = -1;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PogostickPlayer")
        {
            
            Die();
        }
    }
}
