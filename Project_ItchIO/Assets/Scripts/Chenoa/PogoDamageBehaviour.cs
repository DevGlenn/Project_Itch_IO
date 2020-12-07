using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoDamageBehaviour : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision) {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeHit();
        }
    }
}
