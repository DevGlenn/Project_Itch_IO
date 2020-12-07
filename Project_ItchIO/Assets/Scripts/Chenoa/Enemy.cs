using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public GameObject player;

    public void TakeHit() {
        Die();
	}
    private void Die()
    {
        hp = -1;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null && collision.collider == player.PlayerCollider) {
            player.TakeDamage();
		}
    }
}
