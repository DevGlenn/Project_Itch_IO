using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform spellPoint;
    [SerializeField] private int bulletSpeed;
    private List<GameObject> bullets;
    private int allowedShootAmount = 1;
    private int shotAmount = 1;
    private float spellThrust = 5.0f;
    
  
    void Start()
    {
        bullets = new List<GameObject>();
        player = GetComponent<PlayerMovement>();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (shotAmount > 0)
            {
                SpellCast();
                shotAmount--;
            }
            
        }
        if (player.isGrounded == true)
        {
            shotAmount = allowedShootAmount;
        }
    }
    
    void SpellCast()
    {

        Vector2 spellDir = Vector2.up;
        Vector2 velocity = spellThrust * spellDir;

        GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);
        if(player.isGrounded == false)
        {
            GameObject go = Instantiate(spellPrefab, spellPoint.transform.position, spellPrefab.transform.rotation);
            bullets.Add(go);
            go.GetComponent<Rigidbody2D>().AddForce(-transform.up * bulletSpeed, ForceMode2D.Impulse);

        }
        
        
    }
}
