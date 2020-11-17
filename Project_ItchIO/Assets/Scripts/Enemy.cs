using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private GameObject player;


    private void Update()
    {
        
    }

    private void Walking()
    {

    }

    private void Die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PogostickPlayer")
        {
            Die();
        }
    }
}
