using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public GameObject player;

    public void Die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("kmkmkmkmkm");
        if (collision.gameObject.tag == "PogostickPlayer")
        {
            Die();
        }
    }
}
