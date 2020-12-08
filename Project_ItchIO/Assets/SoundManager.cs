using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private static AudioClip playerHit, playerDeath, pickupGet, playerJump;
    private static AudioSource audioSrc;
    void Start()
    {
        playerHit = Resources.Load<AudioClip>("Sound/Sounds/Hurt_sound");
        playerDeath = Resources.Load<AudioClip>("Sound/Sounds/death_sound");
        pickupGet = Resources.Load<AudioClip>("Sound/Sounds/Health_pickup_sound");
        playerJump = Resources.Load<AudioClip>("Sound/Sounds/Jump_sound");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "Hurt_Sound":
                audioSrc.PlayOneShot(playerHit);
                break;
            case "death_sound":
                audioSrc.PlayOneShot(playerDeath);
                break;
            case "Health_pickup_sound":
                audioSrc.PlayOneShot(pickupGet);
                break;
            case "Jump_sound":
                audioSrc.PlayOneShot(playerJump);
                break;

        }
    }
}
