using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    [SerializeField] private Transform player;

    private void Start()
    { 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.position == respawnPos.position)
        {

        }
    }

    private void Checkpoint()
    {

    }
}
