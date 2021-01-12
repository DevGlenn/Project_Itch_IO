using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform background;
    [SerializeField] private Transform player;
    [SerializeField] private float cameraDist = 50f;


    private void Awake()
    {
        GetComponent<Camera>().orthographicSize = ((Screen.height / 2) / cameraDist);
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 15, transform.position.z);
    }
}
