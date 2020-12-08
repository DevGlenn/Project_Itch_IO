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
    private bool isGrounded; //if the player is grounded
    private float chargeValue; // value from 0 to 1


    //[SerializeField] private Image heart1, heart2, heart3;
    [SerializeField] private Image[] hearts;
    private int hp = 3;

    private float jumpPickupTimer = 10f; //de timer van hoe lang de jump pickup actief is als je hem hebt opgepakt
    private bool jumpPickupIsPickedUp = false;

    public Collider2D PlayerCollider { get => playerCollider; }

    private float jumpRotationTimer = 1f; //de tijd hoe lang hij erover doet om te roteren
    private float myRotationValue = 0; //de player zijn rotatievalue 

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
        Jump();

        if (jumpPickupIsPickedUp) //als de jump pickup opgepakt is
        {
            if (jumpPickupTimer <= 0) //als de timer van hoe lang de pickup actief is gelijk is aan of kleiner dan 0 is
            {
                ResetJumpPickupTimer(); //reset de timer functie
            }
            else
            {
                jumpPickupTimer -= Time.deltaTime; //als de timer nog niet voorbij is, ga dan verder met aftellen
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) //als de A knop is ingedrukt
        {
            spriteRenderer.flipX = true; //flip de sprite wel, zodat hij de linker kant opkijkt

        }

        if (Input.GetKeyDown(KeyCode.D)) //als de D knop is ingedrukt
        {
            spriteRenderer.flipX = false; //flip de sprite niet, zodat hij de rechter kant opkijkt
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

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) //als je op spatie klikt en de player staat op de grond
        {
            rb.velocity = new Vector2(10f * FacingDirection, chargeValue * jumpForce); // execute jump
            chargeValue = 0f; // reset charge
            SoundManager.PlaySound("Jump_sound"); //speel de jump sound af
        }
        pogoChargeBar.fillAmount = chargeValue; //pogoChargeBar.fillAmount is gelijk aan de chargeValue

        if (isGrounded == false)
        {
            PlayerRotatesInAir(); //zodra hij in de lucht is dan wordt de functie dat de player draait uitgevoerd
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
        UpdateHitpoints(-1); //de change van de UpdateHitPoints wordt -1 omdat er HP vanaf gaat
        SoundManager.PlaySound("Hurt_Sound"); //speel het damage geluid af
    }

    private void GetHP()
    {
        UpdateHitpoints(1); //de change van de UpdateHitPoints wordt 1 omdat er HP bij gaat
        SoundManager.PlaySound("Health_pickup_sound"); //speel het HP pickup sound geluid af
    }

    private void UpdateHitpoints(int change)
    {
        hp = Mathf.Clamp(hp + change, 0, 3); //hp gaat + de change en zit altijd tussen de 0 en de 3
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp; //zet de harten aan
        }
        if (hp <= 0) //als de player zijn levens onder of gelijk aan 0 is
        {
            Destroy(gameObject); //destroy de player (voor nu)
            //een dood gaan animatie toevoegen en de player opnieuw laten beginnen bij de checkpoints
            SoundManager.PlaySound("death_sound"); //speel het dood gaan geluid af
        }
    }

    private void GetJumpPickup()
    {
        jumpPickupIsPickedUp = true;
        jumpTime = 0.5f; //jump time wordt 2 keer zo snel dus ipv 1 is het 0.5
        jumpPickupTimer = 10f; //de timer van hoe lang de pickup actief is wordt op 10 gezet
    }

    private void ResetJumpPickupTimer()
    {
        jumpPickupIsPickedUp = false;
        jumpTime = 1f; //de jump time wordt op de normale snelheid gezet (=1)
        jumpPickupTimer = 10f; //de timer van hoe lang de pickup actief is wordt op 10 gezet
    }

    private void PlayerRotatesInAir()
    {
        if (myRotationValue < 360) //als de myRotationValue kleiner is dan 360
        {
            myRotationValue += Time.deltaTime * jumpRotationTimer; //de rotatie duurt even lang als er in de jumpRotationTimer wordt aangegeven
            transform.rotation = Quaternion.AngleAxis(myRotationValue, Vector3.back); //de rotatie op de Z as wordt ge-Lerpt naar de myRotationValue 
            myRotationValue += Time.deltaTime * 1000; //myRationValue wordt + de time.deltatime gedaan, keer 1000
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Ground")) //als je collision maakt met een gameobject die de tag "Ground" heeft
        {
            isGrounded = true; //dan wordt de bool isGrounded op true gezet
            rb.velocity = Vector3.zero; //de velocity wordt gereset
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HeartPickup") //als je collision maakt met een gameobject die de tag "HeartPickup" heeft
        {
            GetHP(); //doe de functie GetHP
            Destroy(collision.gameObject); //Destroy het "HeartPickup" object
        }

        if (collision.gameObject.tag == "JumpPickup")//als je collision maakt met een gameobject die de tag "JumpPickup" heeft
        {
            GetJumpPickup(); //doe de functie GetJumpPickup
            Destroy(collision.gameObject); //Destroy het "JumpPickup" object
        }
    }
}
