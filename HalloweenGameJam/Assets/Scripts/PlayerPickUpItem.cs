using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{

    private PlayerCombat playerCombat;
    private DisplayHealth displayHealth;
    [SerializeField] private AudioClip hpGetAudio;

    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
        displayHealth = GetComponent<DisplayHealth>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AGEG");
        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            playerCombat.health += 1;
            if(playerCombat.health > 5) 
            {
                playerCombat.health = 5;

            }
            displayHealth.DisplayHP(playerCombat.health);
            SFXManager.instance.PlaySoundFXClip(hpGetAudio, transform, 0.1f);
            // collect item and apply effect
        }
    }
}
