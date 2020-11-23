using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump")]
    private Rigidbody2D rb;
    [SerializeField] private float jumpTime = 1f; // how long it takes in seconds to charge the bar fully
    [SerializeField] Image pogoChargeBar;
    [SerializeField] private float jumpForce;

    private float chargeValue; // value from 0 to 1 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        #region Jump
        // charge power whenever you hold down the space button
        if (Input.GetKey(KeyCode.Space))
        {
            chargeValue = Mathf.Clamp01(chargeValue + Time.deltaTime / jumpTime); // charge jump and prevent value from exceeding 1
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, chargeValue * jumpForce); // execute jump
            chargeValue = 0f; // reset charge
        }
        pogoChargeBar.fillAmount = chargeValue; 
        #endregion
    }
   
}
