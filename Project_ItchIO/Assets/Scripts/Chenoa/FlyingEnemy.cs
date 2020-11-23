using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    private Vector3 firstPos;
    private Vector3 secondPos;

    [SerializeField] private float speed;
    [SerializeField] private Vector3 journeyLength = new Vector3(5f, 0f, 0f);


    void Start()
    {
        firstPos = transform.position;
        secondPos = transform.position += journeyLength;
    }

    void Update()
    {
        Flying();
    }

    private void Flying()
    {
        transform.position = Vector3.Lerp(firstPos, secondPos, Mathf.PingPong(Time.time * speed, 1.0f));
        
    }

    
}
