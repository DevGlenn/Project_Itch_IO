using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private RectTransform rt;

    
    public float jumpTime = 1f; // how long it takes in seconds to charge the bar fully
    [SerializeField] private Image pogoChargeBar;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private float chargeValue; // value from 0 to 1

    
    [SerializeField] private Image heart1, heart2, heart3;
    private int hp = 3;

    private float jumpPickupTimer = 10f;
    private bool jumpPickupIsPickedUp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        Jump();

        if (jumpPickupIsPickedUp)
        {
            if (jumpPickupTimer <= 0)
            {
                ResetJumpPickupTimer();
            }
            else
            {
                jumpPickupTimer -= Time.deltaTime;
            }
        }
    }
    private void Jump()
    {
        #region Jump
        // charge power whenever you hold down the space button
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            chargeValue = Mathf.Clamp01(chargeValue + Time.deltaTime / jumpTime); // charge jump and prevent value from exceeding 1
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, chargeValue * jumpForce); // execute jump
            chargeValue = 0f; // reset charge
        }
        pogoChargeBar.fillAmount = chargeValue;
        #endregion
    }
    #region TakeDamage/Health
    private void TakeDamage()
    {
        hp--;
        if (hp == 2)
        {
            heart3.enabled = false;
        }
        else if (hp == 1)
        {
            heart2.enabled = false;
        }
        else if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetHP()
    {
        if (hp <= 3)
        {
            hp++;
            if (hp == 1)
            {
                heart1.enabled = true;
            }
            else if (hp == 2)
            {
                heart2.enabled = true;
            }
            else if (hp == 3)
            {
                heart3.enabled = true;
            }
        }
    }
    #endregion
    private void GetJumpPickup()
    {
        jumpPickupIsPickedUp = true;
        jumpTime = 0.5f;
        jumpPickupTimer = 10f;
    }

    private void ResetJumpPickupTimer()
    {
        jumpPickupIsPickedUp = false;
        jumpTime = 1f;
        jumpPickupTimer = 10f;
    }
    #region IsGrounded

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
       

        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HeartPickup")
        {
            GetHP();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "JumpPickup")
        {
            GetJumpPickup();
            Destroy(collision.gameObject);
        }
    }
   
    

}
