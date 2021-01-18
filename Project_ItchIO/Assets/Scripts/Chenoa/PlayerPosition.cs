using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private GameManager gm;
    private PlayerMovement playerScript;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerScript = GetComponent<PlayerMovement>();
        transform.position = gm.lastCheckpointPosition;
    }

    private void Update()
    {
        if (playerScript.isDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
