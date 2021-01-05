using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private RectTransform rectTransform;
    private SpriteRenderer spriteRenderer;


    private float jumpTime = 1f; // how long it takes in seconds to charge the bar fully
    [SerializeField] private Image pogoChargeBar;
    [SerializeField] private float jumpForce;
    [SerializeField] private Collider2D playerCollider;
    private bool isGrounded;
    private float chargeValue; // value from 0 to 1


    //[SerializeField] private Image heart1, heart2, heart3;
    [SerializeField] private Image[] hearts;
    private int hp = 3;

    private float jumpPickupTimer = 10f;
    private bool jumpPickupIsPickedUp = false;

    public Collider2D PlayerCollider { get => playerCollider; }

    private float jumpRotationTimer = 1f;
    private float myRotationValue = 0;

    private Animator animator;

    public bool FacingRight
    {
        get
        {
            return !spriteRenderer.flipX;
        }
    }

    public int FacingDirection
    {
        get
        {
            return spriteRenderer.flipX ? -2 : 2;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.Log(animator.GetBool("IsJumping"));
        animator.SetBool("IsJumping", !isGrounded);


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
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
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
            rb.velocity = new Vector2(10f * FacingDirection, chargeValue * jumpForce); // execute jump
            chargeValue = 0f; // reset charge
            SoundManager.PlaySound("Jump_sound");

        }
        pogoChargeBar.fillAmount = chargeValue;

        if (isGrounded == false)
        {
            PlayerRotatesInAir();
        }
        else if (isGrounded == true) //zodra hij op de grond staat
        {
            myRotationValue = 0; //myRotationValue wordt op 0 gezet
            transform.rotation = Quaternion.AngleAxis(myRotationValue, Vector3.back); //transform.rotation op de z as wordt hetzelfde als de myRotationValue, in dit geval is dat 0
        }
        #endregion
    }


    public void TakeDamage()
    {
        UpdateHitpoints(-1);
        SoundManager.PlaySound("Hurt_Sound");
    }

    private void GetHP()
    {
        UpdateHitpoints(1);
        SoundManager.PlaySound("Health_pickup_sound");
    }

    private void UpdateHitpoints(int change)
    {
        hp = Mathf.Clamp(hp + change, 0, 3); //hp gaat + de change en zit altijd tussen de 0 en de 3
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp; //zet de harten aan
        }
        if (hp == 0)
        {
            Destroy(gameObject);
            SoundManager.PlaySound("death_sound");


        }
    }

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

    private void PlayerRotatesInAir()
    {
        if (myRotationValue < 360) //als de myRotationValue kleiner is dan 360
        {
            myRotationValue += Time.deltaTime * jumpRotationTimer;
            transform.rotation = Quaternion.AngleAxis(myRotationValue, Vector3.back); //de rotatie op de Z as wordt ge-Lerpt naar de myRotationValue 
            myRotationValue += Time.deltaTime * 1000; //myRationValue wordt + de time.deltatime gedaan, keer 1000
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

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
