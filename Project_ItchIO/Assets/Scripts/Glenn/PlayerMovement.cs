using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump")]
     private float jumpForce;
    [SerializeField] private float jumpCharge = 0;
    

    private Rigidbody2D rb;
    private bool jump;
    
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // charge power whenever you hold down the space button
        if (Input.GetKey(KeyCode.Space))
        {
            jumpCharge += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = true;
        }

    }
    private void FixedUpdate()
    {
        if(jump == true)
        {
            jumpForce = 5f * jumpCharge;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jump = false;
            jumpCharge = 0f;
        }
        if(jumpCharge > 1.5f)
        {
            jumpCharge = 1.5f;
        }
    }
}
